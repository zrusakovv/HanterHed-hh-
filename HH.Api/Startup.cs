using HH.ApplicationServices.Services.Implementations;
using HH.ApplicationServices.Services.Interfaces;
using HH.Core;
using HH.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using System.IO;
using HH.Data.SqlServer;
using HH.Identity.Models;
using HH.Identity.Services;
using Microsoft.AspNetCore.Identity;

namespace HH.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();
            services.ConfigureLoggerService();
            services.ConfigureSqlContext(Configuration);
            services.ConfigureSqlContextIdentity(Configuration);
            services.ConfigureRepository();
            services.FluentValidation();
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddScoped<IUserService, UserService>();
            services.AddAuthentication(Configuration);

            services.AutoMapper();

            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen();

            services
                .AddTransient<ICompanyService, CompanyService>()
                .AddTransient<IEmployeeService, EmployeeService>()
                .AddTransient<ISummaryService, SummaryService>()
                .AddTransient<IVacancyService, VacancyService>();
                    
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseCustomErrorHandlingMiddleware();
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Head Hanter");
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
