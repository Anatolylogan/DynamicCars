using Domain.Entities;
using Domain.Сontracts;

namespace Domain.UseCase
{
    public class BasicCostCalculator : ICostCalculator
    {
        private readonly IPricingRepository _pricing;

        public BasicCostCalculator(IPricingRepository pricing)
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

