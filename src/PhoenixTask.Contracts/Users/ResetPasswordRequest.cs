namespace PhoenixTask.Contracts.Users;

public sealed class ResetPasswordRequest
{
    public string Email { get; set; }
    public string Token { get; set; }
    public string Password { get; set; }
}