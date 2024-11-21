
using Domain.Repository;

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
            if (order == null || order.ClientId != clientId)
            {
                throw new Exception("Заказ не найден или доступ запрещен");
            }

            order.Status = "Отменен";
            _orderRepository.Update(order);
        }
    }
}