using ErrorOr;
using MediatR;

namespace Application.Customers.Status.Create
{
    public record CreateCustomerStatusCommand(
        string Description    
    ) : IRequest<ErrorOr<Unit>>;
}
