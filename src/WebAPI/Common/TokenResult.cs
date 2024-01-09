namespace WebAPI.Common
{
    public record TokenResult(
        string Token,
        DateTime Expiration
    );
}
