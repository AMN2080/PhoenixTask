namespace PhoenixTask.Contracts.Emails;

public sealed class ForgetPasswordEmail
{
    public ForgetPasswordEmail(string emailTo, string name, string token)
    {
        EmailTo = emailTo;
        Name = name;
        Token = token;
    }

    public string EmailTo { get; }
    public string Name { get; }
    public string Token { get; }
}
