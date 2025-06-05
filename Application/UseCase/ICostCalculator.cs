using Domain.Entities;

namespace Domain.UseCase
{
    public interface ICostCalculator
    {
        decimal Calculate(Order order);
    }
}