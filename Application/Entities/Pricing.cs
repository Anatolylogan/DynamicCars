namespace Domain.Entities
{
    public class Pricing : Entity
    {
        public decimal BasePrice { get; set; }
        public decimal BrandMultiplier { get; set; }
        public decimal CarpetCost { get; set; }
        public decimal DiscountRate { get; set; }
    }
}
