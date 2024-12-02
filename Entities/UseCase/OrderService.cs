using Domain.Entities;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase
{
    public class OrderService
    {
        private readonly OrderRepository orderRepository;
        private readonly ClientRepository clientRepository;
        public OrderService(OrderRepository orderRepo, ClientRepository clientRepo)
        {
            orderRepository = orderRepo;
            clientRepository = clientRepo;
        }
        //public Order CreateOrder(Client client, List<(string color, string carBrand)> matChoices) //Метод для создание заказа. Принимает данные клиента и ковриков
       // {
///List<Mat> mats = new List<Mat>();
          //  int matId = 1;
           // foreach (var choice in matChoices)
///{
               // mats.Add(new Mat(choice.color, choice.carBrand));
           // }
           // Order order = new Order
            //{
            //    Client = client,
            //   ClientId = client.ClientId,
             //   Mats = mats,
///MatChoices = matChoices ?? new List<(string color, string carBrand)>(),
             //   Status = "Создан"
           // };
           // client.Orders.Add(order);
           // orderRepository.Add(order);
            //return order
      //  }
        
        public void GetAll()
        {
            var orders = orderRepository.GetAll();
            if (orders.Any())
            {
                Console.WriteLine("Список заказов:");
                foreach (var order in orders)
                {
                    Console.WriteLine($"ID: {order.OrderID}, Статус: {order.Status}");
                }
            }
            else
            {
                Console.WriteLine("Нет заказов.");
            }
        }
    }
}
