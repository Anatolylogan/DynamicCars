using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.UseCase
{
    public interface IOrderRepository
    {
        void Add(Order order);
        void Update(Order order);
        void Delete(Order order);
        Order GetByOrderId(int orderId);
        IEnumerable<Order> GetByClientId(int clientId);
        IEnumerable<Order> GetByMakerId(int makerId);
    }
}
