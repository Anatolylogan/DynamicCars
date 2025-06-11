using Application.Entities;
using Application.Сontracts;

namespace Application.UseCase
{
    public class DiscountedCostCalculator : ICostCalculator
    {
        private readonly IPricingRepository _pricing;

        public DiscountedCostCalculator(IPricingRepository pricing)
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

            totalCost *= 1 - _pricing.DiscountRate;
            return totalCost;
        }
    }
}