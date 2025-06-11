using Application.Entities;
using Application.Сontracts;

namespace Application.UseCase
{
    public class CalculateOrderCostUseCase
    {
        private readonly ICostCalculator _basicCostCalculator;
        private readonly ICostCalculator _discountedCostCalculator;

        public CalculateOrderCostUseCase(IPricingRepository pricingRepository)
        {
            _basicCostCalculator = new BasicCostCalculator(pricingRepository);
            _discountedCostCalculator = new DiscountedCostCalculator(pricingRepository);
        }

        public decimal Execute(Order order, string calculatorType)
        {
            var calculator = GetCostCalculator(calculatorType);
            return calculator.Calculate(order);
        }

        private ICostCalculator GetCostCalculator(string type)
        {
            return type.ToLower() switch
            {
                "basic" => _basicCostCalculator,
                "discounted" => _discountedCostCalculator,
                _ => throw new ArgumentException("Неизвестный тип калькулятора")
            };
        }
    }
}