using Domain.Entities;

namespace Infrastructure.Repository
{
    public class MakerRepository : BaseRepository<Maker>
    {
        public MakerRepository(string filePath) : base(filePath) { }

        public Maker GetByName(string name)
        {
            return Items.Find(maker => maker.Name == name);
        }
    }

}
