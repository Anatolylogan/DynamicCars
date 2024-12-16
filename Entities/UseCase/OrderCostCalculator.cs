using Domain.Entities;

namespace Domain.UseCase
{
    public class OrderCostCalculatorUseCase
    {
        public LogHandler Logger { get; set; } 

        public decimal CalculateCost(Order order)
        {
            decimal baseCost = 3000m; 
            decimal carpetCost = 500m;
            decimal totalCost = baseCost + carpetCost;
            Logger?.Invoke($"Расчет стоимости заказа ID {order.Id}: {totalCost}.");
            return totalCost;
        }
    }

}
