using Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Customer>?> GetAllAsync() => await _context.Customers.Include(c => c.CustomerStatus).ToListAsync();

        public async Task<Customer?> GetByIdAsync(CustomerId id) => await _context.Customers.Include(c => c.CustomerStatus).SingleOrDefaultAsync();

        public async Task<bool> ExistsAsync(CustomerId id) => await _context.Customers.AnyAsync(e => e.Id == id);
        
        public void Add(Customer customer) => _context.Customers.Add(customer);

        public void Update(Customer customer)  => _context.Customers.Update(customer);

        public void Delete(Customer customer) => _context.Customers.Remove(customer);
    }
}