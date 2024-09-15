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
        public const string InviteMember = "workspace/{workspaceId:guid}/member/{userId:guid}/add";
        public const string RemoveMember = "workspace/{workspaceId:guid}/member/{userId:guid}/remove";
    }
    public static class Projects
    {
        public const string GetWorkSpaceProjects = "workspace/{workspaceId:guid}/projects";
        public const string Create = "workspace/{workspaceId:guid}/project";
        public const string GetById = "workspace/{workspaceId:guid}/project/{projectId:guid}";
        public const string Update = "workspace/{workspaceId:guid}/project/{projectId:guid}";
        public const string Remove = "workspace/{workspaceId:guid}/project/{projectId:guid}";
    }
    public static class Boards
    {
        public const string GetProjectBoards = "board/get/{projectId:guid}";
        public const string Create = "board/{projectId:guid}";
        public const string GetById = "board/{board:guid}";
        public const string Update = "board/{board:guid}";
        public const string Remove = "board/{board:guid}";
    }
}