using Domain.Entities;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase
{
    public class MakerService
    {
        private readonly OrderRepository orderRepository;
        public MakerService(OrderRepository orderRepo)
        {
            orderRepository = orderRepo;
        }
        public void CompleteMaking(Order order) //Метод для заверние изготовление
        {
            order.Status = "Изготовлен";
        }
    }
}
