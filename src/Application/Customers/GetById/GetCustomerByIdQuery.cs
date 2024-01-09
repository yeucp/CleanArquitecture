using Application.Customers.Common;
using ErrorOr;
using MediatR;

namespace Application.Customers.GetById
{
    public record GetCustomerByIdQuery(Guid Id) : IRequest<ErrorOr<CustomerResponse>>;
}
