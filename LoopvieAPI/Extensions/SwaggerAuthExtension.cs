using WLAPI.Middlewares;

namespace WLAPI.Extensions
{
    public static class SwaggerAuthExtension
    {
        public static IApplicationBuilder UseSwaggerAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SwaggerBasicAuthMiddleware>();
        }
    }
}
