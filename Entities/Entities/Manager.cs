using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Manager
    {
        public static int nextIdManager = 1;
        public int ManagerId { get; set; }
        public string Name { get; set; }
        public List<Order> AssignedOrders { get; set; } = new List<Order>();
    }
}
