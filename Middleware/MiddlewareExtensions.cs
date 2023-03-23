using Tasks.Middleware;

namespace Tasks.Middleware;
public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseLogMiddleware(
        this IApplicationBuilder app
    )
    {
        return app.UseMiddleware<LogMiddleware>();

    }


}