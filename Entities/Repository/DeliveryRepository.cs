using Domain.Entities;

namespace Infrastructure.Repository
{
    public class DeliveryRepository : BaseRepository<Delivery>
    {
        public DeliveryRepository(string filePath) : base(filePath) { }
    }
}

