using Application.Entities;

namespace Application.Сontracts
{
    public interface IStoreRepository : IRepository<Store>
    {
        void AddToStore(Order order);
        List<Order> GetAll();
    }
}