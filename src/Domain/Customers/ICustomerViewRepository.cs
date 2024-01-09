namespace Domain.Customers
{
    public interface ICustomerViewRepository
    {
        Task<List<CustomersView>> GetAllAsync();
    }
}
