namespace PhoenixTask.Contracts.Authentication;

public sealed class TokenResponse(string token)
{
    public string Token { get; } = token;
}
