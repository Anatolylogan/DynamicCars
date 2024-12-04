using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repository
{
    namespace Domain.Repository
    {
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
    }
}