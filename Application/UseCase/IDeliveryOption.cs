namespace Domain.UseCase
{
    public interface IDeliveryOption
    {
        string GetDeliveryDetails();
        decimal GetCost();
    }
}