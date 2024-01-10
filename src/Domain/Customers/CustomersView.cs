using Newtonsoft.Json;

namespace Domain.Customers
{
    public class CustomersView
    {
        [JsonProperty("uid")]
        public Guid Id { get; private set; }
        [JsonProperty("first_name")]
        public string Name { get; private set; } = string.Empty;
        [JsonProperty("email")]
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
