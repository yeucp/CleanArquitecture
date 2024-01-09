using Application.Customers.Common;
using Domain.Customers;
using ErrorOr;
using MediatR;

namespace Application.Customers.GetById
{
    public sealed class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, ErrorOr<CustomerResponse>>
    {

        private ICustomerRepository _customerRepository;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task<ErrorOr<CustomerResponse>> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
        {
            if(await _customerRepository.GetByIdAsync(new CustomerId(query.Id)) is not Customer customer)
            {
                return Error.NotFound("Customer.NotFound", "The customer with provide Id was not found");
            }

            return new CustomerResponse(
                customer.Id.Value,
                customer.Name,
                customer.LastName,
                $"{customer.Name} {customer.FullName}",
                customer.PhoneNumber.Value,
                customer.Active,
                customer.CustomerStatus
            );
        }
    }
}
