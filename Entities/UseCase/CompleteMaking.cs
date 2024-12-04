using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase
{
    public class CompleteMakingUseCase
    {
        private readonly OrderRepository _orderRepository;

        public CompleteMakingUseCase(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Execute(int orderId)
        {
            var order = _orderRepository.GetByOrderId(orderId);
            if (order == null)
            {
                throw new Exception("Заказ не найден");
            }
            order.Status = "Выполнен";
            _orderRepository.Update(order, o => o.OrderID == orderId); 
        }
    }
}

