using Domain.Entities;
using Infrastructure.Repository;

namespace Domain.UseCase
{
    public class BasicCostCalculator : ICostCalculator
    {
        private readonly PricingRepository _pricing;

        public BasicCostCalculator(PricingRepository pricing)
        {
            _pricing = pricing;
        }

        public decimal Calculate(Order order)
        {
            decimal totalCost = _pricing.BasePrice;
            foreach (var item in order.Items)
            {
                totalCost += _pricing.CarpetCost;
                totalCost *= _pricing.BrandMultiplier;
            }
            return totalCost;
        }
    }
}

