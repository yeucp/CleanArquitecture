namespace Domain.CustomerStatuses
{
    public interface ICustomerStatusRepository
    {
        Task<List<CustomerStatus>?> GetAlAsync();
        Task<CustomerStatus?> GetByIdAsync(CustomerStatusId customerStatusId);

        void Add(CustomerStatus customerStatus);
    }
}
