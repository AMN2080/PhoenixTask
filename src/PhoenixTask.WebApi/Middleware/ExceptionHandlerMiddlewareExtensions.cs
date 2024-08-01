namespace PhoenixTask.WebApi.Middleware;

internal static class ExceptionHandlerMiddlewareExtensions
{
    internal static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        => builder.UseMiddleware<ExceptionHandlerMiddleware>();
}