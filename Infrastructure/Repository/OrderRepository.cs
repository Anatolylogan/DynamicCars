using Domain.Entities;
using Domain.Сontracts;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(IOptions<RepositorySettings> options)
        : base(options.Value.OrdersFilePath) { }

        public OrderRepository(string filePath) : base(filePath) { }

        public List<Order> GetByClientId(int clientId)
        {
            return Items.Where(order => order.ClientId == clientId).ToList();
        }
        public List<Order> GetByMakerId(int makerId)
        {
            return Items.Where(order => order.MakerId == makerId).ToList();
        }
        public void Delete(Order order)
        {
            Items.Remove(order);
        }
        public List<Order> GetOrdersByStatus(Func<Order, bool> filter)
        {
            return Items.Where(filter).ToList();
        }
    }
}
