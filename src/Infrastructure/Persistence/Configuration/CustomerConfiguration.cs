﻿using Domain.Customers;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasConversion(
                customerId => customerId.Value,
                value => new CustomerId(value));

            builder.Property(c => c.Name).HasMaxLength(50);

            builder.Property(c => c.LastName).HasMaxLength(50);

            builder.Property(c => c.Email).HasMaxLength(255);

            builder.Ignore(c => c.FullName);

            builder.HasIndex(c => c.Email);

            builder.Property(c => c.PhoneNumber).HasConversion(
                phoneNumber => phoneNumber.Value,
                value => PhoneNumber.Create(value)!).HasMaxLength(9);

            builder.Property(c => c.Active);

            builder.HasOne(c => c.CustomerStatus).WithMany().HasForeignKey(c => c.CustomerStatusId);
        }
    }
}
