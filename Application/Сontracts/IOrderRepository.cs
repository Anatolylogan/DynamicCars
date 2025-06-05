using Domain.Entities;

namespace Domain.Сontracts
{
    public interface IOrderRepository : IRepository<Order>
    {
        void Delete(Order order);
        List<Order> GetByClientId(int clientId);
        List<Order> GetByMakerId(int makerId);
        List<Order> GetOrdersByStatus(Func<Order, bool> filter);
    }
}