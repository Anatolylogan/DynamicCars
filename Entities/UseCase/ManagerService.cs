using Domain.Entities;
using Domain.Сontracts;

namespace Domain.UseCase
{
    public class ManagerService
    {
        private readonly IManagerRepository managerRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IMakerRepository makerRepository;
        public ManagerService(IOrderRepository orderRepo, IMakerRepository makerRepo, IManagerRepository managerRepo)
        {
            orderRepository = orderRepo;
            makerRepository = makerRepo;
            managerRepository = managerRepo;
        }

        public void AssignMaker(Order order, Maker maker)
        {
            order.Maker = maker;
            order.MakerId = maker.Id;
            order.Status = OrderStatus.InProgress;
        }
    }
}
