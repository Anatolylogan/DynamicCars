namespace Domain.Entities
{
    public class Client
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();

    }
}
