using Application.Entities;
using Application.Сontracts;

namespace Infrastructure.Repository
{
    public class PricingRepository : BaseRepository<Pricing> , IPricingRepository
    {
        public PricingRepository(string filePath) : base(filePath) { }
        public decimal BasePrice => 3000m;
        public decimal CarpetCost => 500m;
        public decimal BrandMultiplier => 1.2m;
        public decimal DiscountRate => 0.1m;
    }
}