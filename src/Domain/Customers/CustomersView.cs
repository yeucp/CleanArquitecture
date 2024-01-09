namespace Domain.Customers
{
    public class CustomersView
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;

        public CustomersView()
        {
        }

        public CustomersView(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}
