using Domain.Entities;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var order = _orderRepository.GetById(orderId);
            if (order == null)
            {
                throw new Exception("Заказ не найден");
            }

            var delivery = new Delivery
            {
                DeliveryId = new Random().Next(1, 1000),
                Address = address,
                Order = order
            };

            _deliveryRepository.Add(delivery);
            order.Status = "На доставке";
            _orderRepository.Update(order);
        }
    }
}