using Domain.Repository;
using Domain.Repository.Domain.Repository;

namespace Domain.UseCase
{
    public class SendToStoreUseCase
    {
        private readonly OrderRepository _orderRepository;
        private readonly StoreRepository _storeRepository;

        public SendToStoreUseCase(OrderRepository orderRepository, StoreRepository storeRepository)
        {
            _orderRepository = orderRepository;
            _storeRepository = storeRepository;
        }

        public void Execute(int orderId)
        {
            var order = _orderRepository.GetByOrderId(orderId);
            if (order == null)
            {
                throw new Exception("Заказ не найден");
            }
            _storeRepository.AddToStore(order);
            order.Status = "На складе";
            _orderRepository.Update(order, o => o.OrderID == order.OrderID);

            Console.WriteLine($"Заказ с ID {order.OrderID} отправлен на склад.");
        }
    }
}

