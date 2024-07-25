using PhoenixTask.Domain.Abstractions.Primitives;

namespace PhoenixTask.Application.Core.Errors;

internal static class ValidationErrors
{
    internal static class Login
    {
        internal static Error UsernameIsRequired => new("Login.UsernameIsRequired", "The email is required.");

        internal static Error PasswordIsRequired => new("Login.PasswordIsRequired", "The password is required.");
    }
}
