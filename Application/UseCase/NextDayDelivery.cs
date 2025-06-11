namespace Application.UseCase
{
    public class NextDayDelivery : IDeliveryOption
    {
        public string GetDeliveryDetails()
        {
            return "Доставка на следующий день.";
        }

        public decimal GetCost()
        {
            return 2500m; 
        }
    }
}
