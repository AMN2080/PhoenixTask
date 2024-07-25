using PhoenixTask.Domain.Abstractions.Primitives;

namespace PhoenixTask.Domain.Abstractions.Exceptions;
public class DomainException(Error error) : Exception(error.Message)
{
    public Error Error { get; } = error;
}