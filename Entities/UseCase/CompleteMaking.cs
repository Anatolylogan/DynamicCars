using Infrastructure.Repository;

namespace Domain.UseCase
{
    public class CompleteMakingUseCase
    {
        private readonly OrderRepository _orderRepository;
        private readonly OrderProcessor _orderProcessor;
        public LogHandler Logger { get; set; }
        public CompleteMakingUseCase(OrderRepository orderRepository,OrderProcessor orderProcessor)
        {
            _orderRepository = orderRepository;
            _orderProcessor = orderProcessor;
        }

        public void Execute(int orderId)
        {
            var order = _orderRepository.GetById(orderId);
            if (order == null)
            {
                Logger?.Invoke($"Ошибка: заказ с ID {orderId} не найден.");
                throw new Exception("Заказ не найден.");
            }

            order.Status = OrderStatus.Completed;
            _orderRepository.Update(order);


            Logger?.Invoke($"Заказ с ID {orderId} завершен.");
        }
    }
}

