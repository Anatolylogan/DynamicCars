using Domain.Entities;
using Domain.Сontracts;

namespace Infrastructure.Repository
{
    public class DeliveryRepository : BaseRepository<Delivery> , IDeliveryRepository
    {
        public DeliveryRepository(string filePath) : base(filePath) { }
    }
}

