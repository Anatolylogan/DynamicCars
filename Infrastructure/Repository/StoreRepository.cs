using Domain.Entities;
using Domain.Сontracts;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repository;
public class StoreRepository : BaseRepository<Store> , IStoreRepository
{
    public StoreRepository(IOptions<RepositorySettings> options)
       : base(options.Value.StoreFilePath) { }

    public StoreRepository(string filePath) : base(filePath) { }
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