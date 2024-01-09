using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Extensions
{
    public static class MIgrationExtension
    {
        public static void ApplyMigration(this WebApplication app) 
        { 
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
