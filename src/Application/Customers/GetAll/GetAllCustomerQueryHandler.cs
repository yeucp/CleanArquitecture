using Application.Customers.Common;
using Domain.Customers;
using ErrorOr;
using MediatR;

namespace Application.Customers.GetAll
{
    public sealed class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomersQuery, ErrorOr<IReadOnlyList<CustomerResponse>>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetAllCustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        async Task<ErrorOr<IReadOnlyList<CustomerResponse>>> IRequestHandler<GetAllCustomersQuery, ErrorOr<IReadOnlyList<CustomerResponse>>>.Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllAsync();

            return customers.Select(customer => new CustomerResponse(
                customer.Id.Value,
                customer.Name,
                customer.LastName,
                $"{customer.Name} {customer.LastName}",
                customer.PhoneNumber.Value,
                customer.Active,
                customer.CustomerStatus
            )).ToList();
        }
    }
}
