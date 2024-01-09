using Application.Data;
using Domain.CustomerStatuses;
using Domain.Customers;
using Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
    {
        private readonly IPublisher _publisher;

        public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)
        {
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }

        public DbSet<Customer> Customers { get ; set ; }
        public DbSet<CustomerStatus> CustomerStatuses { get ; set ; }
        public DbSet<CustomersView> ViewCustomers { get ; set ; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            modelBuilder.Entity<CustomersView>(entity => entity.ToView("ViewCustomers"));
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken()) 
        {
            var domainEvents = ChangeTracker.Entries<AgregateRoot>()
                .Select(e => e.Entity)
                .Where(e => e.GetDomainEvents().Any())
                .SelectMany(e => e.GetDomainEvents());

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in domainEvents)
            { 
                await _publisher.Publish(domainEvent, cancellationToken);
            }

            return result;
        }
    }
}
