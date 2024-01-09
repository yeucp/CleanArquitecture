using Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    internal class CustomerViewRepository : ICustomerViewRepository
    {
        private ApplicationDbContext _context;

        public CustomerViewRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<CustomersView>> GetAllAsync() => await _context.ViewCustomers.ToListAsync();
    }
}
