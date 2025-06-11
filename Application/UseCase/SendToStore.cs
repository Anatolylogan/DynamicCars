using Application.Сontracts;

namespace Application.UseCase
{
    public class SendToStoreUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IStoreRepository _storeRepository;

        public SendToStoreUseCase(IOrderRepository orderRepository, IStoreRepository storeRepository)
        {
            _orderRepository = orderRepository;
            _storeRepository = storeRepository;
        }

        public void Execute(int orderId)
        {
            var order = _orderRepository.GetById(orderId);
            if (order == null)
            {
                throw new Exception("Заказ не найден");
            }
            _storeRepository.AddToStore(order);
            order.Status = OrderStatus.Warehouse;
            _orderRepository.Update(order);

            Console.WriteLine($"Заказ с ID {order.Id} отправлен на склад.");
        }
    }
}

