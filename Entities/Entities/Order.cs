using Domain.UseCase;

namespace Domain.Entities
{
    public class Order : Entity
    {
        public int ClientId { get; set; }
        public int MakerId { get; set; }
        public OrderStatus Status { get; set; }
        public Maker Maker { get; set; }
        public string ClientEmail { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}


