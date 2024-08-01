namespace PhoenixTask.Contracts.Users;

public sealed class ChangePasswordRequest
{
    public string Password { get; set; }
    public Guid UserId { get; set; }
}
