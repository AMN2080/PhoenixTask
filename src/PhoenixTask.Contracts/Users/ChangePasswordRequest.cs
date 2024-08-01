namespace PhoenixTask.Contracts.Users;

public sealed class ChangePasswordRequest
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public Guid UserId { get; set; }
}
