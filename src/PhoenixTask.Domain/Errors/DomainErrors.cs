using PhoenixTask.Domain.Abstractions.Primitives;

namespace PhoenixTask.Domain.Errors;

public static class DomainErrors
{
    public static class FirstName
    {
        public static Error NullOrEmpty => new("FirstName.NullOrEmpty", "The first name is required.");

        public static Error LongerThanAllowed => new("FirstName.LongerThanAllowed", "The first name is longer than allowed.");
    }
    public static class LastName
    {
        public static Error NullOrEmpty => new("LastName.NullOrEmpty", "The last name is required.");

        public static Error LongerThanAllowed => new("LastName.LongerThanAllowed", "The last name is longer than allowed.");
    }
    public static class UserName
    {
        public static Error NullOrEmpty => new("UserName.NullOrEmpty", "The UserName is required.");

        public static Error LongerThanAllowed => new("UserName.LongerThanAllowed", "The UserName is longer than allowed.");

        public static Error InvalidFormat => new("UserName.InvalidFormat", "The UserName format is invalid.");
    }
    public static class Email
    {
        public static Error NullOrEmpty => new("Email.NullOrEmpty", "The email is required.");

        public static Error LongerThanAllowed => new("Email.LongerThanAllowed", "The email is longer than allowed.");

        public static Error InvalidFormat => new("Email.InvalidFormat", "The email format is invalid.");
    }
    public static class Password
    {
        public static Error NullOrEmpty => new("Password.NullOrEmpty", "The password is required.");

        public static Error TooShort => new("Password.TooShort", "The password is too short.");

        public static Error MissingUppercaseLetter => new(
            "Password.MissingUppercaseLetter",
            "The password requires at least one uppercase letter.");

        public static Error MissingLowercaseLetter => new(
            "Password.MissingLowercaseLetter",
            "The password requires at least one lowercase letter.");

        public static Error MissingDigit => new(
            "Password.MissingDigit",
            "The password requires at least one digit.");

        public static Error MissingNonAlphaNumeric => new(
            "Password.MissingNonAlphaNumeric",
            "The password requires at least one non-alphanumeric.");
    }
    public static class User
    {
        public static Error NotFound => new Error("User.NotFound", "The user with the specified identifier was not found.");

        public static Error InvalidPermissions => new Error(
            "User.InvalidPermissions",
            "The current user does not have the permissions to perform that operation.");

        public static Error DuplicateEmail => new Error("User.DuplicateEmail", "The specified email is already in use.");
        public static Error DuplicateUsername => new Error("User.DuplicateUsername", "The specified username is already in use.");

        public static Error CannotChangePassword => new Error(
            "User.CannotChangePassword",
            "The password cannot be changed to the specified password.");
        public static Error UsernameNotFound => new(
            "User.UsernameNotFound",
            "Invalid credential.");
        public static Error UserNotFound => new(
            "User.UserNotFound",
            "Invalid credential.");
        public static Error BadPassword => new(
            "User.BadPassword",
            "Invalid credential.");
    }
    public static class General
    {
        public static Error UnProcessableRequest => new Error(
            "General.UnProcessableRequest",
            "The server could not process the request.");

        public static Error ServerError => new Error("General.ServerError", "The server encountered an unrecoverable error.");
    }
    public static class Name
    {
        public static Error LongerThanAllowed => new("Name.LongerThanAllowed", "The Name is longer than allowed.");
        public static Error NullOrEmpty => new("Name.NullOrEmpty", "The Name is required.");
    }
    public static class Color
    {
        public static Error NullOrEmpty => new("Color.NullOrEmpty", "The Color is required.");
    }
    public static class WorkSpace
    {
        public static Error NotFound => new Error("WorkSpace.NotFound", "The workspace with the specified identifier was not found.");
    }
}