﻿using Domain.Entities;
using Infrastructure.Repository;

namespace Domain.UseCase
{
    public class CompleteMakingUseCase
    {
        private readonly OrderRepository _orderRepository;

        public LogHandler Logger { get; set; }
        public event Action<Order> OrderReady;

        public CompleteMakingUseCase(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Execute(int orderId, string clientEmail)
        {
            var order = _orderRepository.GetById(orderId);
            if (order == null)
            {
                Logger?.Invoke($"Ошибка: заказ с ID {orderId} не найден.");
                throw new Exception("Заказ не найден.");
            }
            order.Status = OrderStatus.Completed;
            order.ClientEmail = clientEmail;
            _orderRepository.Update(order);
            Logger?.Invoke($"Заказ с ID {orderId} завершен.");
            OnOrderReady(order);
        }
        protected virtual void OnOrderReady(Order order)
        {
            OrderReady?.Invoke(order);
        }
    }
}
