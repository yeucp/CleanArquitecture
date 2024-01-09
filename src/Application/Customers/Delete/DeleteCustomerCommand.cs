using ErrorOr;
using MediatR;
namespace Application.Customers.Delete
{
    public record DeleteCustomerCommand(Guid id) : IRequest<ErrorOr<Unit>>;
}