namespace PhoenixTask.Contracts.Users;

public sealed class CreateUserRequest
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
