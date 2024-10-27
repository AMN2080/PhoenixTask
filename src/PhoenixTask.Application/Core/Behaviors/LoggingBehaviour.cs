using MediatR;
using Microsoft.Extensions.Logging;

namespace PhoenixTask.Application.Core.Behaviors;

internal sealed class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class, IRequest<TResponse>
        where TResponse : class
{
    private readonly ILogger<Mediator> _logger;

    public LoggingBehaviour(ILogger<Mediator> logger) => _logger = logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"An error occur on process {request} !");
            throw exception;
        }
    }
}