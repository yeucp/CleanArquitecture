using Application.CustomerView.Common;
using ErrorOr;
using MediatR;

namespace Application.CustomerVIew.GetAll
{
    public record GetCustomerViewQuery() : IRequest<ErrorOr<IReadOnlyList<CustomerViewResult>>>;
}
