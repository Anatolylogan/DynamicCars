using Domain.Entities;
using Infrastructure.Repository;

namespace Domain.UseCase
{
    public class FilterOrdersByStatusUseCase
    {
        private readonly OrderRepository _orderRepository;

        public FilterOrdersByStatusUseCase(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> Execute(OrderStatus status)
        {
            return _orderRepository.GetOrdersByStatus(order => order.Status == status);
        }
    }
}
