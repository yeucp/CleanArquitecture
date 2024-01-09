using Domain.Customers;
using Domain.CustomerStatuses;
using Domain.DomainErrors;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Customers.Update
{
    public sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ErrorOr<Unit>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerStatusRepository _customerStatusRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, ICustomerStatusRepository customerStatusRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _customerStatusRepository = customerStatusRepository;
            _unitOfWork = unitOfWork;
        }

        async Task<ErrorOr<Unit>> IRequestHandler<UpdateCustomerCommand, ErrorOr<Unit>>.Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            if(!await _customerRepository.ExistsAsync(new CustomerId(command.Id)))
                return Errors.Customer.CustomerNotFound;

            if(PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber)
                return Errors.Customer.PhoneNumberWithBadFormat;

            if (await _customerStatusRepository.GetByIdAsync(new CustomerStatusId(command.CustomerStatus)) is not CustomerStatus customerStatus)
                return Errors.Customer.CustomerStatusNotFound;

            Customer customer = Customer.UpdateCustomer(
                command.Id,
                command.Name,
                command.LastName,
                command.Email,
                command.Active,
                phoneNumber,
                customerStatus
            );

            _customerRepository.Update(customer);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
