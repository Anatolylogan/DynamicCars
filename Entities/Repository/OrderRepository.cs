
using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.UseCase;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(string filePath) : base(filePath) { }

    public Order GetByOrderId(int orderId)
    {
        return Items.Find(order => order.OrderID == orderId);
    }

    public IEnumerable<Order> GetByClientId(int clientId)
    {
        return Items.Where(order => order.ClientId == clientId);
    }

    public IEnumerable<Order> GetByMakerId(int makerId)
    {
        return Items.Where(order => order.MakerId == makerId);
    }
    public void Update(Order order)
    {
        var existingOrder = GetByOrderId(order.OrderID);
        if (existingOrder != null)
        {
            existingOrder.Status = order.Status;
            existingOrder.ClientId = order.ClientId;
            existingOrder.MakerId = order.MakerId;
        }
    }
    public void Delete(Order order)
    {
        Items.Remove(order); 
    }
}
