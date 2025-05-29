using Domain.Сontracts;

namespace Domain.UseCase
{
    public class AssignMakerToOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMakerRepository _makerRepository;

        public AssignMakerToOrderUseCase(IOrderRepository orderRepository, IMakerRepository makerRepository)
        {
            _orderRepository = orderRepository;
            _makerRepository = makerRepository;
        }

        public void Execute(int orderId, string Name)
        { 
            var order = _orderRepository.GetById(orderId);
            if (order == null)
            {
                throw new Exception("Заказ не найден");
            }
            var maker = _makerRepository.GetByName(Name);
            if (maker == null)
            {
                throw new Exception("Изготовитель не найден");
            }
            order.Maker = maker;
            order.MakerId = maker.Id;
            order.Status = OrderStatus.New;
            _orderRepository.Update(order); 
        }
    }
}
