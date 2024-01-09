using Domain.CustomerStatuses;

namespace Application.Customers.Common
{
    public record CustomerResponse(
        Guid Id,
        string Name,
        string LastName,
        string FullName,
        string PhoneNumber,
        bool Active,
        CustomerStatus CustomerStatus
    );
}
