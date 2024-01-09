using ErrorOr;
using MediatR;
namespace Application.Customers.Exists
{
    public record class ExistsCustomerQuery(Guid id) : IRequest<ErrorOr<bool>>;
}