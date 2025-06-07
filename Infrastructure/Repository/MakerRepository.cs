using Domain.Entities;
using Domain.Сontracts;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repository
{
    public class MakerRepository : BaseRepository<Maker>, IMakerRepository
    {
        public MakerRepository(IOptions<RepositorySettings> options)
        : base(options.Value.MakersFilePath) { }
        public MakerRepository(string filePath) : base(filePath) { }

        public Maker GetByName(string name)
        {
            return Items.Find(maker => maker.Name == name);
        }
    }

}
