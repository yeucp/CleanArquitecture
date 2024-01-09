using Domain.CustomerStatuses;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Customers
{
    public sealed class Customer : AgregateRoot
    {
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Customer() {}

        public Customer(CustomerId id, string name, string lastName, string email, bool active, PhoneNumber phoneNumber, CustomerStatus status)
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Email = email;
            Active = active;
            PhoneNumber = phoneNumber;
            CustomerStatus = status;
        }

        public CustomerId Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public bool Active { get; private set; }
        public string FullName => $"{Name} {LastName}";
        public PhoneNumber PhoneNumber {get; private set; }
        public CustomerStatusId CustomerStatusId { get; private set; }
        public CustomerStatus CustomerStatus { get; private set; }

        public static Customer UpdateCustomer(Guid id, string name, string lastName, string email, bool active, PhoneNumber phoneNumber, CustomerStatus customerStatus) {
            return new Customer(new CustomerId(id), name, lastName, email, active, phoneNumber, customerStatus);
        }

        public static Customer newCustomer(Customer customer)
        {
            return new Customer(
                new CustomerId(customer.Id.Value),
                customer.Name,
                customer.LastName,
                customer.Email, 
                customer.Active, 
                customer.PhoneNumber,
                new CustomerStatus(customer.CustomerStatus.Id, customer.CustomerStatus.Description)
            );
        }
    }
}
