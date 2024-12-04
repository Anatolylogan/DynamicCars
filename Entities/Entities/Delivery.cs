using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Delivery
    {
        private static int _idCounter = 0;
        public int DeliveryId { get; set; }
        public int OrderId {  get; set; }
        public string Address { get; set; }
        public Order Order { get; set; }
    }
}