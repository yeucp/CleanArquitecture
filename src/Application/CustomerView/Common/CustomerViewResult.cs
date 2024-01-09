namespace Application.CustomerView.Common
{
    public record CustomerViewResult(
        Guid Id,
        string Name,
        string Email
    );
}
