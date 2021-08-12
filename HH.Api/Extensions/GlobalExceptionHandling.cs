using HH.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace HanterHed_hh_.Extensions
{
    public class GlobalExceptionHandling
    {
        private readonly RequestDelegate next;
        private readonly ILoggerManager logger;
        public GlobalExceptionHandling(RequestDelegate next, ILoggerManager logger)
        {
            this.next = next;
            this.logger = logger;
        }
        public async Task Invoke(HttpContext context, IWebHostEnvironment env)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError($"Что-то пошло не так: {ex}");

                await HandleExceptionAsync(context, ex, env);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception ex, IWebHostEnvironment env)
        {
            var code = HttpStatusCode.BadRequest;

            var includeDetails = env.IsEnvironment("Development");
            var title = includeDetails ? "An error occurred: " + ex.Message : "An error occurred";
            var details = includeDetails ? ex.ToString() : null;

            var problem = new ProblemDetails
            {
                Status = (int?)code,
                Title = title,
                Detail = details
            };

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)code;

            var result = JsonConvert.SerializeObject(problem);
            return context.Response.WriteAsync(result);
        }

    }
}
