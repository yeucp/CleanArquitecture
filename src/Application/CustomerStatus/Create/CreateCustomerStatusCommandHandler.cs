using Domain.CustomerStatuses;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Customers.Status.Create
{
    public sealed class CreateCustomerStatusCommandHandler : IRequestHandler<CreateCustomerStatusCommand, ErrorOr<Unit>>
    {
        private ICustomerStatusRepository _customerStatusRepository;
        private IUnitOfWork _unitOfWork;

        public CreateCustomerStatusCommandHandler(ICustomerStatusRepository customerStatusRepository, IUnitOfWork unitOfWork)
        {
            _customerStatusRepository = customerStatusRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<ErrorOr<Unit>> Handle(CreateCustomerStatusCommand command, CancellationToken cancellationToken)
        {
            var customerStatus = new CustomerStatus(
                new CustomerStatusId(Guid.NewGuid()),
                command.Description
            );

            _customerStatusRepository.Add(customerStatus);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
