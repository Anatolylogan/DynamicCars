﻿using Application.Entities;
using Application.Сontracts;

namespace Application.UseCase
{
    public class FilterOrdersByStatusUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public FilterOrdersByStatusUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> Execute(OrderStatus status)
        {
            return _orderRepository.GetOrdersByStatus(order => order.Status == status);
        }
    }
}
