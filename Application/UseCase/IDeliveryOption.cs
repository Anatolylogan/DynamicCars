namespace Application.UseCase
{
    public interface IDeliveryOption
    {
        string GetDeliveryDetails();
        decimal GetCost();
    }
}