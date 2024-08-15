using PhoenixTask.Domain.Abstractions.Primitives;

namespace PhoenixTask.Application.Core.Errors;

internal static class ValidationErrors
{
    internal static class Login
    {
        internal static Error UsernameIsRequired => new("Login.UsernameIsRequired", "The email is required.");

        internal static Error PasswordIsRequired => new("Login.PasswordIsRequired", "The password is required.");
    }
    internal static class CreateWorkSpace
    {
        internal static Error UserIdIsRequired => new("CreateWorkSpace.UserIdIsRequired", "The user identifier is required.");
        internal static Error NameIsRequired => new("CreateWorkSpace.NameIsRequired", "The name is required.");
        internal static Error ColorIsRequired => new("CreateWorkSpace.ColorIsRequired", "The color is required.");
    }
    internal sealed class UpdateWorkSpace
    {
        internal static Error WorkSpaceIdIsRequired => new("WorkSpaceIdIsRequired.UserIdIsRequired", "The werkspace identifier is required.");
        internal static Error NameIsRequired => new("UpdateWorkSpace.NameIsRequired", "The name is required.");
        internal static Error ColorIsRequired => new("UpdateWorkSpace.ColorIsRequired", "The color is required.");
    }
}
