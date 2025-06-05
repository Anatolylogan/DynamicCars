using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase
{
    public class ExpressDelivery : IDeliveryOption
    {
        public string GetDeliveryDetails()
        {
            return "Экспресс доставка: 1-2 рабочих дня.";
        }

        public decimal GetCost()
        {
            return 1500m; 
        }
    }
}
