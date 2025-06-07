using Domain.Entities;
using Domain.Сontracts;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repository
{
    public class DeliveryRepository : BaseRepository<Delivery> , IDeliveryRepository
    {
        public DeliveryRepository(IOptions<RepositorySettings> options)
       : base(options.Value.DeliveryFilePath) { }
        public DeliveryRepository(string filePath) : base(filePath) { }
    }
}

