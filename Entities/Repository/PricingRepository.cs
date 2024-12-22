namespace Infrastructure.Repository
{
    public class PricingRepository
    {
        public decimal BasePrice => 10000m;
        public decimal CarpetCost => 500m;
        public decimal BrandMultiplier => 1.2m;
        public decimal DiscountRate => 0.1m;
    }
}