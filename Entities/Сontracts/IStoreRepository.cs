using Domain.Entities;

namespace Domain.Сontracts
{
    public interface IStoreRepository : IRepository<Store>
    {
        void AddToStore(Order order);
        List<Order> GetAll();
    }
}