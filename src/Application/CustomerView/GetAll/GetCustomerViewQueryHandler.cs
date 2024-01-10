using Application.CustomerView.Common;
using Application.CustomerVIew.GetAll;
using Domain.Customers;
using ErrorOr;
using MediatR;

namespace Application.CustomerView.GetAll
{
    public sealed class GetCustomerViewQueryHandler : IRequestHandler<GetCustomerViewQuery, ErrorOr<IReadOnlyList<CustomerViewResult>>>
    {
        private ICustomerViewRepository _customerViewRepository;

        public GetCustomerViewQueryHandler(ICustomerViewRepository customerViewRepository)
        {
            _customerViewRepository = customerViewRepository;
        }

        async Task<ErrorOr<IReadOnlyList<CustomerViewResult>>> IRequestHandler<GetCustomerViewQuery, ErrorOr<IReadOnlyList<CustomerViewResult>>>.Handle(GetCustomerViewQuery request, CancellationToken cancellationToken)
        {
            var customerViewResult = await _customerViewRepository.GetApiAsync();

            return customerViewResult.Select(customer => new CustomerViewResult(
                customer.Id,
                customer.Name,
                customer.Email
            )).ToList();
        }
    }
}
