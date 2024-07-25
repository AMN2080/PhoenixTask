using PhoenixTask.Domain.Abstractions.Primitives;

namespace PhoenixTask.WebApi.Contract;

public class ApiErrorResponse
{
    public ApiErrorResponse(IReadOnlyCollection<Error> errors) => Errors = errors;

    public IReadOnlyCollection<Error> Errors { get; }
}