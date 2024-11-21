using Domain.Entities;
using Domain.Repository;

namespace Domain.UseCase
{
    public class SendOrderToStore
    {
        private readonly StoreService storeService;
        private readonly OrderRepository orderRepository;

        public SendOrderToStore (StoreService storeService, OrderRepository orderRepository)
        {
            this.storeService = storeService;
            this.orderRepository = orderRepository;
        }

        public void Execute(int orderId)
        {
            Order order = orderRepository.GetById(orderId);
            if (order == null)
                throw new Exception("Заказ не найден.");

            storeService.SendTOStore(order);
        }
    }
}
