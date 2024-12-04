using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class DeliveryRepository : BaseRepository<Delivery>
    {
        public DeliveryRepository(string filePath) : base(filePath) { }

        public Delivery GetByOrderId(int orderId)
        {
            return Items.Find(delivery => delivery.OrderId == orderId);
        }
    }

}

