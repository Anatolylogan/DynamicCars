using Domain.UseCase;

namespace DynamicCarsNew.Infrastructure
{
    public class PickupOption : IDeliveryOption
    {
        public string GetDeliveryDetails()
        {
            return "Самовывоз";
        }

        public decimal GetCost()
        {
            return 0m;
        }
    }
}