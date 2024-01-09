using Domain.Login;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private ApplicationDbContext _context;

        public LoginRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> ValidateUser(string username, string password) 
        {
            var loginResult = await _context.Database.SqlQuery<int>(
                $"EXEC [dbo].[SP_Login] {username}, {password}"
            ).ToListAsync();

            return loginResult.FirstOrDefault() == 1;
        }
    }
}
