using Domain.Entities;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//public class DeliveryService
//{
//    private readonly DeliveryRepository _deliveryRepository;
//    private readonly OrderRepository _orderRepository;

//    public DeliveryService(DeliveryRepository deliveryRepository, OrderRepository orderRepository)
//    {
//        _deliveryRepository = deliveryRepository;
//        _orderRepository = orderRepository;
//    }

//    public void SendToDelivery(int orderId, string address)
//    {
//        var order = _orderRepository.GetByOrderId(orderId);
//        if (order == null)
//        {
//            throw new Exception("Заказ не найден");
//        }

//        var delivery = new Delivery
//        {
//            OrderId = orderId,
//            Address = address
//        };

//        _deliveryRepository.Add(delivery);
//        order.Status = "Отправлен на доставку";
//        _orderRepository.Update(order);
//    }

//    public void CompleteDelivery(int orderId)
//    {
//        var order = _orderRepository.GetByOrderId(orderId);
//        if (order == null)
//        {
//            throw new Exception("Заказ не найден");
//        }

//        order.Status = "Доставлен";
//        _orderRepository.Update(order);
//    }
//}
