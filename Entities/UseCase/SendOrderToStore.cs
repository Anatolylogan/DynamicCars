using Domain.Entities;
using Domain.Repository;

namespace Domain.UseCase
{
    public class SendOrderToStore
    {
        private readonly StoreService storeService;
        private readonly OrderRepository orderRepository;

        public SendOrderToStore(StoreService storeService, OrderRepository orderRepository)
        {
            this.storeService = storeService;
            this.orderRepository = orderRepository;
        }

        public void Execute(int orderId)
        {
            var orders = orderRepository.GetAll();
            if (!orders.Any())
            {
                Console.WriteLine("Нет доступных заказов.");
                return;
            }

            Console.WriteLine("Доступные заказы:");
            foreach (var order in orders)
            {
                Console.WriteLine($"ID: {order.OrderID}, Клиент: {order.Client.Name}, Статус: {order.Status}");
            }
        }
    }
}
