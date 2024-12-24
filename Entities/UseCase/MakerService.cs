using Domain.Entities;
using Infrastructure.Repository;


namespace Domain.UseCase
{
    public class MakerService
    {
        private readonly OrderRepository orderRepository;
        public MakerService(OrderRepository orderRepo)
        {
            orderRepository = orderRepo;
        }
      
    }
}
