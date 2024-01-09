using Domain.Customers;
using Domain.Primitives;

namespace Domain.CustomerStatuses
{
    public sealed class CustomerStatus : AgregateRoot
    {
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private CustomerStatus() { }

        public CustomerStatus(CustomerStatusId id)
        {
            Id = id;
        }

        public CustomerStatus(CustomerStatusId id, string description)
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Id = id;
            Description = description;
        }

        public CustomerStatusId Id { get; private set; }
        public string Description { get; private set; } = string.Empty;
    }
}
