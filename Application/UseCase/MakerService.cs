using Application.Entities;
using Application.Сontracts;

namespace Application.UseCase
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
