using ErrorOr;
using MediatR;

namespace Application.Customers.Update
{
    public record UpdateCustomerCommand(
        Guid Id,
        string Name,
        string LastName,
        string Email,
        bool Active,
        string PhoneNumber,
        Guid CustomerStatus
    ) : IRequest<ErrorOr<Unit>>;
}
