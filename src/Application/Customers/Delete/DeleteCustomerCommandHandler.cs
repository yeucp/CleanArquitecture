using Domain.Customers;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Customers.Delete
{
    public sealed class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ErrorOr<Unit>>
    {
        private ICustomerRepository _customerRepository;
        private IUnitOfWork _unitOfWork;

        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
        {
            if(await _customerRepository.GetByIdAsync(new CustomerId(command.id)) is not Customer customer)
            {
                return Error.NotFound("Customer.Id", "The customer with the provided Id was not found");
            }

            _customerRepository.Delete(customer);

            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
