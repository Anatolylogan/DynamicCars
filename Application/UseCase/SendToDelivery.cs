using Application.Entities;
using Application.Сontracts;

namespace Application.UseCase
{
    public class SendToDeliveryUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDeliveryRepository _deliveryRepository;

        public SendToDeliveryUseCase(IOrderRepository orderRepository, IDeliveryRepository deliveryRepository)
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
                OrderId = order.Id,
                Address = address
            };
            _deliveryRepository.Add(delivery);
            order.Status = OrderStatus.OnDelivery;
            _orderRepository.Update(order);

            Console.WriteLine($"Заказ с ID {order.Id} отправлен на доставку.");
        }
    }
}