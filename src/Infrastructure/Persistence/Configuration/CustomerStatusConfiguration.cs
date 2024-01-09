using Domain.CustomerStatuses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class CustomerStatusConfiguration : IEntityTypeConfiguration<CustomerStatus>
    {
        public void Configure(EntityTypeBuilder<CustomerStatus> builder)
        {
            builder.HasKey(cs => cs.Id);

            builder.Property(cs => cs.Id).HasConversion(
                customerStatusId => customerStatusId.Value,
                value => new CustomerStatusId(value)
            );

            builder.Property(cs => cs.Description).HasMaxLength(200);

            //builder.HasMany(cs => cs.Customers).WithOne(c => c.CustomerStatus).HasForeignKey(c => c.CustomerStatusId);
        }
    }
}
