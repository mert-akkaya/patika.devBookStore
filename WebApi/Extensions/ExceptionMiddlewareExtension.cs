using Microsoft.AspNetCore.Builder;
using WebApi.Middleware;

namespace WebApi.Extensions{
    public static class ExceptionMiddlewareExtension{
        public static void CustomExceptionMiddleware(this IApplicationBuilder app){
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}