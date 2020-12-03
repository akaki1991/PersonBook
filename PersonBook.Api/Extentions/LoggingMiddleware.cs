using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Threading.Tasks;

namespace PersonBook.Api.Extentions
{
    public class LoggingMiddleware
    {
        readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                var exceptionHandlerFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
                httpContext.Response.StatusCode = 500;
                if (exceptionHandlerFeature != null)
                {
                    Log.Error(exceptionHandlerFeature.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            await _next(httpContext);
        }
    }
}
