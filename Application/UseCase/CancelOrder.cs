using Application.Entities;
using Application.Сontracts;
using System;

namespace Application.UseCase
{
    public class CancelOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public CancelOrderUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Execute(int orderId, int clientId)
        {
            var order = _orderRepository.GetById(orderId);

            if (order == null)
                throw new Exception("Заказ не найден.");

            if (order.ClientId != clientId)
                throw new Exception("Этот заказ не принадлежит указанному клиенту.");

            _orderRepository.Delete(order);
        }
    }
}


