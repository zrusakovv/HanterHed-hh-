using HH.Core;
using HH.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HH.Api
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate next;
        private readonly ILoggerManager logger;
        public GlobalExceptionHandler(RequestDelegate next, ILoggerManager logger)
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
            catch (EntityNotFoundException exception)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var details = new ProblemDetails
                {
                    Detail = exception.Message
                };

                await context.Response.WriteAsJsonAsync(details);
            }
            catch (Exception exception)
            {
                logger.LogError($"Что-то пошло не так: {exception}");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var details = new ProblemDetails
                {
                    Detail = exception.Message
                };

                await context.Response.WriteAsJsonAsync(details);
            }
        }
    }
}
