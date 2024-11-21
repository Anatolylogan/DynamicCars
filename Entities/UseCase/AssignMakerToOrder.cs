
using Domain.Entities;
using Domain.Repository;

namespace Domain.UseCase
{
    public class AssignMakerToOrderUseCase
    {
        private readonly OrderRepository _orderRepository;
        private readonly MakerRepository _makerRepository;

        public AssignMakerToOrderUseCase(OrderRepository orderRepository, MakerRepository makerRepository)
        {
            _orderRepository = orderRepository;
            _makerRepository = makerRepository;
        }

        public void Execute(int orderId, string makerName)
        {
            var order = _orderRepository.GetById(orderId);
            if (order == null)
            {
                throw new Exception("Заказ не найден");
            }

            var maker = new Maker
            {
                MakerId = new Random().Next(1, 1000),
                Name = makerName
            };

            _makerRepository.Add(maker);
            order.Maker = maker;
            order.MakerId = maker.MakerId;
            order.Status = "Назначен изготовитель";
            _orderRepository.Update(order);
        }
    }
}