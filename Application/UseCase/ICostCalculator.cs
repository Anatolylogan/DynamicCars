using Application.Entities;

namespace Application.UseCase
{
    public interface ICostCalculator
    {
        decimal Calculate(Order order);
    }
}