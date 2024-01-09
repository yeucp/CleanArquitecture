using ErrorOr;

namespace Domain.DomainErrors
{
    public static partial class Errors {

        public static class Customer {
            public static Error PhoneNumberWithBadFormat => Error.Validation("Customer.PhoneNumber", "Phone number has not valid format.");
            public static Error CustomerNotFound => Error.Validation("Customer.NotFound", "The customer with provide Id was not found.");
            public static Error CustomerStatusNotFound => Error.Validation("Customer.CustomerStatus", "Customer status not found.");
        }
    }
}
