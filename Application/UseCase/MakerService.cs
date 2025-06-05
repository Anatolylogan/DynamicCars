using Domain.Entities;
using Domain.Сontracts;

namespace Domain.UseCase
{
    public class MakerService
    {
        private readonly IOrderRepository orderRepository;
        public MakerService(IOrderRepository orderRepo)
        {
            orderRepository = orderRepo;
        }
      
    }
}
