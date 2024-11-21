using Domain.Entities;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase
{
    public class StoreService
    {
        public void SendTOStore(Order order) //Метод для  отправки заказа на склад
        {
            if (order == null)
            {
                Console.WriteLine("Невозможно отправить заказ на склад, так как заказ не существует.");
                return;
            }
            if (order.MatChoices == null)
            {
                order.MatChoices = new List<(string color, string carBrand)>();
            }
            order.Status = "На складе";
        }
    }
}
