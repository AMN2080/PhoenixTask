namespace PhoenixTask.Contracts.Users;

public sealed class ResetPasswordRequest
{
    public string Token { get; set; }
    public string Password { get; set; }
}