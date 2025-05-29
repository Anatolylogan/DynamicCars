using Domain.Entities;
using Domain.Сontracts;

namespace Infrastructure.Repository
{
    public class MakerRepository : BaseRepository<Maker>, IMakerRepository
    {
        public MakerRepository(string filePath) : base(filePath) { }

        public Maker GetByName(string name)
        {
            return Items.Find(maker => maker.Name == name);
        }
    }

}
