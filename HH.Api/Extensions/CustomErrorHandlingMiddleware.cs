using HanterHed_hh_.ErrorModel;
using HH.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace HanterHed_hh_.Extensions
{
    public class CustomErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public CustomErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context, IWebHostEnvironment env)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, env);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception ex, IWebHostEnvironment env)
        {
            var code = HttpStatusCode.InternalServerError;

            if (ex is NotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }
            else if (ex is ApplicationLayerException)
            {
                code = HttpStatusCode.BadRequest;
            }
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
