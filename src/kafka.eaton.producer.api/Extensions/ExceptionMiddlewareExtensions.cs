using kafka.eaton.producer.api.middlewares;
using Microsoft.AspNetCore.Builder;

namespace kafka.eaton.producer.api.extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
