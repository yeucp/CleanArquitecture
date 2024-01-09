using Domain.Customers;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using Domain.DomainErrors;
using Domain.CustomerStatuses;

namespace Application.Customers.Create
{
    public sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ErrorOr<Unit>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerStatusRepository _customerStatusRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, ICustomerStatusRepository customerStatusRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _customerStatusRepository = customerStatusRepository ?? throw new ArgumentNullException(nameof(customerStatusRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            if (PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber)
                return Errors.Customer.PhoneNumberWithBadFormat;

            if(await _customerStatusRepository.GetByIdAsync(new CustomerStatusId(command.CustomerStatus)) is not CustomerStatus customerStatus)
                return Errors.Customer.CustomerStatusNotFound;

            var customer = new Customer(
                new CustomerId(Guid.NewGuid()),
                command.Name,
                command.LastName,
                command.Email,
                true,
                phoneNumber,
                customerStatus
            );

            _customerRepository.Add(customer);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
