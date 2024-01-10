using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json;

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

        public async Task<List<CustomersView>> GetApiAsync()
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetAsync("https://random-data-api.com/api/users/random_user?size=3");

            if(!result.IsSuccessStatusCode)
                return new List<CustomersView>();

            var jsonResult = await result.Content.ReadAsStringAsync();

            var customers = JsonConvert.DeserializeObject<List<CustomersView>>(jsonResult);

            return customers ?? new List<CustomersView>();
        }
    }
}
