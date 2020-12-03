using Microsoft.AspNetCore.Builder;

namespace PersonBook.Api.Extentions
{
    public static class LoggingMiddlwareExtensions
    {
        public static IApplicationBuilder UseGlobalLogging(this IApplicationBuilder builder)
        {
            return builder.UseExceptionHandler(apiBuilder => apiBuilder.UseMiddleware<LoggingMiddleware>());
        }
    }
}
