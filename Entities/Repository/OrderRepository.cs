using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class OrderRepository
    {
        private readonly List<Order> orders = new List<Order>();
        public void Add(Order order)//Добавление заказа
        {
            orders.Add(order);
        }
        public Order GetById(int id)
        {
            return orders.FirstOrDefault(o => o.OrderID == id);
        }
        public List<Order> GetAll()//Все заказы
        {
            return orders;
        }
        public void Update(Order order)//Обновление заказа
        {
            var existingOrder = GetById(order.OrderID);
            if (existingOrder != null)
            {
                existingOrder.Mats = order.Mats;
                existingOrder.Client = order.Client;
                existingOrder.Maker = order.Maker;
                existingOrder.Status = order.Status;
            }
        }
    }
}
