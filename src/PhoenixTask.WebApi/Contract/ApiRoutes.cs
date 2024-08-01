namespace PhoenixTask.WebApi.Contract;

public static class ApiRoutes
{
    public static class Authentication
    {
        public const string Login = "authentication/login";
        public const string ForgetPassword = "authentication/reset-password";
        public const string ResetPassword = "authentication/reset-password/set-password";
    }

    public static class Users
    {
        public const string ChangePassword = "users/{userId:guid}/change-passwrod";
        public const string Create = "users/create";
    }
}