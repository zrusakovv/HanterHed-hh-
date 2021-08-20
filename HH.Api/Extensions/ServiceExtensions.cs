using System;
using System.Text;
using FluentValidation.AspNetCore;
using HH.Data.Abstractions;
using HH.Data.SqlServer;
using HH.Identity.Settings;
using HH.Infrastructure;
using HH.Infrastructure.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using DbContext = HH.Data.SqlServer.DbContext;

namespace HH.Api
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddTransient<ILoggerManager, LoggerManager>();
        }
        
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbContext>(opts =>
                opts.UseSqlServer(
                    configuration.GetConnectionString("sqlConnection"),
                    builder => builder.MigrationsAssembly("HH.Api")
                )
            );
        }
        
        public static void ConfigureSqlContextIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opts =>
                opts.UseSqlServer(
                    configuration.GetConnectionString("sqlConnectionIdentity"),
                    builder => builder.MigrationsAssembly("HH.Api")
                )
            );
        }

        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddTransient<IRepository>(provider => new Repository<DbContext>(provider.GetService<DbContext>()));
        }

        public static void AutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CompanyMappingProfile).Assembly);
        }

        public static void FluentValidation(this IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(s =>
                {
                    s.RegisterValidatorsFromAssemblyContaining<Startup>();
                    s.DisableDataAnnotationsValidation = false;
                });
        }

        public static void UseCustomErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandler>();
        }

        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.GetSection(nameof(JWT));

            services
                .AddOptions<JWT>()
                .Bind(config);
            
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                        
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes
                                (configuration.GetSection(nameof(JWT))
                                    .GetValue<string>(nameof(JWT.Key))))

                    };
                });
        }
    }
}