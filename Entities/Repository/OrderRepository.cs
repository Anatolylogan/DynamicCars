using Domain.Entities;

namespace Infrastructure.Repository
{
    public class OrderRepository : BaseRepository<Order>
    {
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
    }
}