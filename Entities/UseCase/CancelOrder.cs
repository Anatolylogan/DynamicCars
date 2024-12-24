using Infrastructure.Repository;

namespace Domain.UseCase
{
    public class CancelOrderUseCase
    {
        private readonly OrderRepository _orderRepository;

        public CancelOrderUseCase(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Execute(int orderId, int clientId)
        {
            var order = _orderRepository.GetById(orderId);
            if (order == null)
            {
                throw new Exception("Заказ не найден.");
            }
            if (order.ClientId != clientId)
            {
                throw new Exception("Этот заказ не принадлежит указанному клиенту.");
            }
            _orderRepository.Delete(order);

            Console.WriteLine($"Заказ с ID {orderId} успешно отменен.");
        }
    }
}


