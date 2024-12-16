namespace Domain.Entities
{
    public class Manager : Entity
    {
        public string Name { get; set; }
        public List<Order> AssignedOrders { get; set; } = new List<Order>();
    }
}
