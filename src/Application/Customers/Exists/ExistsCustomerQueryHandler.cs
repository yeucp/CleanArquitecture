using Domain.Customers;
using ErrorOr;
using MediatR;

namespace Application.Customers.Exists
{
    public sealed class ExistsCustomerQueryHandler : IRequestHandler<ExistsCustomerQuery, ErrorOr<bool>>
    {
        private ICustomerRepository _customerRepository;

        public ExistsCustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        async Task<ErrorOr<bool>> IRequestHandler<ExistsCustomerQuery, ErrorOr<bool>>.Handle(ExistsCustomerQuery query, CancellationToken cancellationToken)
        {
            bool existsCustomer = await _customerRepository.ExistsAsync(new CustomerId(query.id));
            return existsCustomer;
        }
    }
}
