using Domain.UseCase;

namespace Domain.Entities
{
    public class Order : Entity
    {
        public int ClientId { get; set; }
        public int MakerId { get; set; }
        public OrderStatus Status { get; set; }
        public Maker Maker { get; set; }
        public string CarBrand { get; set; }
        public string CarpetColor { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public decimal TotalCost { get; set; }
        public string ClientEmail { get; set; }
    }

}


