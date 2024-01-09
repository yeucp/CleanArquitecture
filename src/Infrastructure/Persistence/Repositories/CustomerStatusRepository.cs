using Domain.CustomerStatuses;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    internal class CustomerStatusRepository : ICustomerStatusRepository
    {
        private ApplicationDbContext _context;

        public CustomerStatusRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<CustomerStatus>?> GetAlAsync() => await _context.CustomerStatuses.ToListAsync();

        public async Task<CustomerStatus?> GetByIdAsync(CustomerStatusId customerStatusId) => await _context.CustomerStatuses.SingleOrDefaultAsync(cs => cs.Id == customerStatusId);
        public void Add(CustomerStatus customerStatus) => _context.Add(customerStatus);
    }
}
