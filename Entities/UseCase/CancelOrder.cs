using Domain.Entities;
using Domain.Сontracts;
using System;

namespace Domain.UseCase
{
    public class CancelOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public CancelOrderUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Order Execute(int orderId, int clientId)
        {
            var order = _orderRepository.GetById(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException("Заказ не найден.");
            }

            if (order.ClientId != clientId)
            {
                throw new InvalidOperationException("Этот заказ не принадлежит указанному клиенту.");
            }

            _orderRepository.Delete(order);

            Console.WriteLine($"Заказ с ID {orderId} успешно отменен.");

            return order;
        }
    }
}



