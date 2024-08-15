namespace PhoenixTask.WebApi.Contract;

public static class ApiRoutes
{
    public static class Authentication
    {
        public const string Login = "authentication/login";
        public const string Create = "authentication/create";
    }

    public static class Users
    {
        public const string ChangePassword = "users/{userId:guid}/change-password";
        public const string ForgetPassword = "users/reset-password";
        public const string ResetPassword = "users/reset-password/set-password";
    }
    public static class WorkSpace
    {
        public const string GetAllWorkSpaces = "workspace/";
        public const string Create = "workspace/create";
        public const string GetById = "workspace/{workspaceId:guid}";
        public const string Update = "workspace/{workspaceId:guid}";
        public const string Remove = "workspace/{workspaceId:guid}";
    }
}