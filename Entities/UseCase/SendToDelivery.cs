using Domain.Entities;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase
{
    namespace Domain.UseCase
    {
        public class SendToDeliveryUseCase
        {
            private readonly OrderRepository _orderRepository;
            private readonly DeliveryRepository _deliveryRepository;

            public SendToDeliveryUseCase(OrderRepository orderRepository, DeliveryRepository deliveryRepository)
            {
                _orderRepository = orderRepository;
                _deliveryRepository = deliveryRepository;
            }

            public void Execute(int orderId, string address)
            {
                var order = _orderRepository.GetByOrderId(orderId);
                if (order == null)
                {
                    throw new Exception("Заказ не найден");
                }
                var delivery = new Delivery
                {
                    OrderId = order.OrderID, 
                    Address = address
                };
                _deliveryRepository.Add(delivery);
                order.Status = "На доставке";
                _orderRepository.Update(order, o => o.OrderID == order.OrderID);

                Console.WriteLine($"Заказ с ID {order.OrderID} отправлен на доставку.");
            }
        }
    }
}