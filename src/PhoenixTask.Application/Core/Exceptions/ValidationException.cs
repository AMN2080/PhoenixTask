using FluentValidation.Results;
using PhoenixTask.Domain.Abstractions.Primitives;

namespace PhoenixTask.Application.Core.Exceptions;
public sealed class ValidationException : FluentValidation.ValidationException
{
    public ValidationException(IEnumerable<ValidationFailure> failures)
        : base("One or more validation failures has occurred.") =>
        Errors = failures
            .Distinct()
            .Select(failure => new Error(failure.ErrorCode, failure.ErrorMessage))
            .ToList();

    public new IReadOnlyCollection<Error> Errors { get; }
}