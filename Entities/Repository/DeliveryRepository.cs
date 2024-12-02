using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class DeliveryRepository
    {
        private readonly List<Delivery> deliveries = new List<Delivery>();
        public void Add(Delivery delivery)
        {
            deliveries.Add(delivery);
        }
        public Delivery GetById(int id)
        {
            return deliveries.FirstOrDefault(d => d.DeliveryId == id);
        }
        public List<Delivery> GetAll()
        {
            return deliveries;
        }
    }
}

