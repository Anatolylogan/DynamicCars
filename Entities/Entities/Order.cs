using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        private static int nextOrderID = 1;
        public int OrderID { get; private set; }
        public List<Mat> Mats { get; set; } = new List<Mat>();
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int MakerId { get; set; }
        public Maker Maker { get; set; }
        public string Status { get; set; }
    }
}
