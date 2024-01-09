namespace Application.Customers.Status.Common
{
    public record CustomerStatusResponse(
        Guid Id,
        string Description
    );
}
