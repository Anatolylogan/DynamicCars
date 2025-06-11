using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase
{
    public class StandardDelivery : IDeliveryOption
    {
        public string GetDeliveryDetails()
        {
            return "Стандартная доставка: 5-7 рабочих дней.";
        }

        public decimal GetCost()
        {
            return 500m; 
        }
    }
}
