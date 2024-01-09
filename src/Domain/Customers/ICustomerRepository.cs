namespace Domain.Customers
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(CustomerId id);
        Task<List<Customer>?> GetAllAsync();
        Task<bool> ExistsAsync(CustomerId id);
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
    }
}
