using Domain.Entities;

namespace Infrastructure.Repository;
public class StoreRepository
{
    private readonly List<Order> storeOrders = new List<Order>();
    public void AddToStore(Order order)
    {
        storeOrders.Add(order);
    }
    public List<Order> GetAll()
    {
        return storeOrders;
    }
}