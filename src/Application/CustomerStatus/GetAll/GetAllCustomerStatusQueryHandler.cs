using Application.Customers.Status.Common;
using Domain.CustomerStatuses;
using Domain.Primitives;
using ErrorOr;
using MediatR;
namespace Application.Customers.Status.GetAll
{
    public sealed class GetAllCustomerStatusQueryHandler : IRequestHandler<GetAllCustomerStatusQuery, ErrorOr<IReadOnlyList<CustomerStatusResponse>>>
    {
        private ICustomerStatusRepository _customerStatusRepository;

        public GetAllCustomerStatusQueryHandler(ICustomerStatusRepository customerStatusRepository, IUnitOfWork unitOfWork)
        {
            _customerStatusRepository = customerStatusRepository;
        }

        async Task<ErrorOr<IReadOnlyList<CustomerStatusResponse>>> IRequestHandler<GetAllCustomerStatusQuery, ErrorOr<IReadOnlyList<CustomerStatusResponse>>>.Handle(GetAllCustomerStatusQuery query, CancellationToken cancellationToken)
        {
            var customerStatuses = await _customerStatusRepository.GetAlAsync();

            return customerStatuses.Select(customerStatus => new CustomerStatusResponse(
                customerStatus.Id.Value,
                customerStatus.Description
            )).ToList();
        }
    }
}
