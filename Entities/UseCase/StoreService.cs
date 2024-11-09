using Domain.Entities;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase
{
    public class StoreService
    {
        private readonly StoreRepository storeRepository;
        private Store store;

        public StoreService(StoreRepository storeRepo)
        {
            storeRepository = storeRepo;
        }
      
        public StoreService(Store store)
        {
            this.store = store;
        }
        public void SendTOStore(Order order) //Метод для  отправки заказа на склад
        {
            store.Orders.Add(order);
            order.Status = "На складе";
        }
    }
}
