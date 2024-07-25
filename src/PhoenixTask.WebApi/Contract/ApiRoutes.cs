namespace PhoenixTask.WebApi.Contract;

public static class ApiRoutes
{
    public static class Authentication
    {
        public const string Login = "authentication/login";

        public const string Register = "authentication/register";
    }

    public static class Users
    {
        public const string ChangePassword = "users/{userId:guid}/change-passwrod";
    }
}