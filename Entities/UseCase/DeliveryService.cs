using Domain.Entities;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase
{
    public class DeliveryService
    {
        private readonly DeliveryRepository deliveryRepository;
        private readonly OrderRepository orderRepository;
        public DeliveryService(DeliveryRepository deliveryRepo, OrderRepository orderRepo)
        {
            deliveryRepository = deliveryRepo;
            orderRepository = orderRepo;
        }

        public void SendToDelivery(Order order, string address) // Метод для отправки товара на доставку
        {
            order.Status = "Отправлен на доставку";
            Delivery delivery = new Delivery()
            {
                Order = order,
                Address = address
            };
        }
        public void CompleteDelivery(Order order) // Метод для завершение доставки
        {
            order.Status = "Доставлен";
        }
    }

}
