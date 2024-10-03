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
    internal sealed class CreateProject
    {
        internal static Error NameIsRequired => new("CreateProject.NameIsRequired", "The name is required.");
        internal static Error WorkSpaceIdIsRequired => new("CreateProject.UserIdIsRequired", "The werkspace identifier is required.");
    }
    internal sealed class UpdateProject
    {
        internal static Error NameIsRequired => new("UpdateProject.NameIsRequired", "The name is required.");
    }
    internal sealed class CreateBoard
    {
        internal static Error NameIsRequired => new("CreateBoard.NameIsRequired", "The name is required.");
        internal static Error ColorIsRequired => new("CreateBoard.ColorIsRequired", "The color is required.");
    }
    internal sealed class UpdateBoard
    {
        internal static Error NameIsRequired => new("UpdateBoard.NameIsRequired", "The name is required.");
        internal static Error ColorIsRequired => new("UpdateBoard.ColorIsRequired", "The color is required.");
    }
    internal sealed class CreateTask
    {
        internal static Error DescriptionIsRequierd => new("CreateTask.DescriptionIsRequierd", "The description is required.");
    }
    internal sealed class UpdateTask
    {
        internal static Error DescriptionIsRequierd => new("UpdateTask.DescriptionIsRequierd", "The description is required.");
    }
    internal sealed class UpdateUser
    {
        internal static Error HaveAtLeastOnePropertyNotNull => new("UpdateUser.HaveAtLeastOnePropertyNotNull", "At least one of FirstName, LastName, or PhoneNumber must be provided.");
    }
}
