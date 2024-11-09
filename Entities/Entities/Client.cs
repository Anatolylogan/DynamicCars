namespace Domain.Entities
{
    public class Client
    {
        private static int nextId = 1;
        public int ClientId { get; private set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();

    }
}
