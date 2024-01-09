using ErrorOr;
using MediatR;
using Application.Customers.Common;

namespace Application.Customers.GetAll
{
    public record GetAllCustomersQuery() : IRequest<ErrorOr<IReadOnlyList<CustomerResponse>>>;
}
