using Application.Сontracts;

namespace Application.UseCase
{
    public class SendOrderToStore
    {
        private readonly IOrderRepository orderRepository;

        public SendOrderToStore(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public void Execute(int orderId)
        {
            var order = orderRepository.GetById(orderId);
            if (order == null)
            {
                Console.WriteLine($"Заказ с ID {orderId} не найден.");
                return;
            }
            if (order.Status != OrderStatus.Completed)
            {
                Console.WriteLine($"Заказ с ID {orderId} не готов для отправки на склад. Текущий статус: {order.Status}.");
                return;
            }         
            order.Status = OrderStatus.Warehouse;
            orderRepository.Update(order);

            Console.WriteLine($"Заказ с ID {orderId} успешно отправлен на склад.");
        }
    }
}
