using Application.Customers.Status.Common;
using ErrorOr;
using MediatR;

namespace Application.Customers.Status.GetAll
{
    public record GetAllCustomerStatusQuery() : IRequest<ErrorOr<IReadOnlyList<CustomerStatusResponse>>>;
}
