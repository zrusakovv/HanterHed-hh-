using HanterHed_hh_.Extensions;
using HH.Data.Abstractions;
using HH.Data.SqlServer;
using HH.Infrastructure;
using HH.Infrastructure.Mapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DbContext = HH.Data.DbContext;

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

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {

            });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddScoped<ILoggerManager, LoggerManager>();

        public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration) =>
                services.AddDbContext<DbContext>(opts =>
                    opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b =>
                        b.MigrationsAssembly("HH.Api")));

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void AutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CompanyMappingProfile).Assembly);
            services.AddAutoMapper(typeof(EmployeeMappingProfile).Assembly);
            services.AddAutoMapper(typeof(SummaryMappingProfile).Assembly);
            services.AddAutoMapper(typeof(VacancyMappingProfile).Assembly);
        }

        public static void UseCustomErrorHandlingMiddleware(this IApplicationBuilder app, ILoggerManager logger) =>
            app.UseMiddleware<CustomErrorHandlingMiddleware>(logger);
    }
}
