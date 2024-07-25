using PhoenixTask.Application.Core.Exceptions;
using PhoenixTask.Domain.Abstractions.Primitives;
using PhoenixTask.Domain.Core.Exceptions;
using PhoenixTask.Domain.Errors;
using PhoenixTask.WebApi.Contract;
using System.Net;
using System.Text.Json;
namespace PhoenixTask.WebApi.Middleware;

internal class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Invokes the exception handler middleware with the specified <see cref="HttpContext"/>.
    /// </summary>
    /// <param name="httpContext">The HTTP httpContext.</param>
    /// <returns>The task that can be awaited by the next middleware.</returns>
    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred: {Message}", ex.Message);

            await HandleExceptionAsync(httpContext, ex);
        }
    }

    /// <summary>
    /// Handles the specified <see cref="Exception"/> for the specified <see cref="HttpContext"/>.
    /// </summary>
    /// <param name="httpContext">The HTTP httpContext.</param>
    /// <param name="exception">The exception.</param>
    /// <returns>The HTTP response that is modified based on the exception.</returns>
    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        (HttpStatusCode httpStatusCode, IReadOnlyCollection<Error> errors) = GetHttpStatusCodeAndErrors(exception);

        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = (int)httpStatusCode;

        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        string response = JsonSerializer.Serialize(new ApiErrorResponse(errors), serializerOptions);

        await httpContext.Response.WriteAsync(response);
    }

    private static (HttpStatusCode httpStatusCode, IReadOnlyCollection<Error>) GetHttpStatusCodeAndErrors(Exception exception) =>
           exception switch
           {
               ValidationException validationException => (HttpStatusCode.BadRequest, validationException.Errors),
               DomainException domainException => (HttpStatusCode.BadRequest, new[] { domainException.Error }),
               _ => (HttpStatusCode.InternalServerError, new[] { DomainErrors.General.ServerError })
           };
}
internal static class ExceptionHandlerMiddlewareExtensions
{
    /// <summary>
    /// Configure the custom exception handler middleware.
    /// </summary>
    /// <param name="builder">The application builder.</param>
    /// <returns>The configured application builder.</returns>
    internal static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        => builder.UseMiddleware<ExceptionHandlerMiddleware>();
}