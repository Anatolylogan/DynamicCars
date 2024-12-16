using Domain.Entities;

namespace Infrastructure.Repository
{
    public class ManagerRepository : BaseRepository<Manager>
    {
        public ManagerRepository(string filePath) : base(filePath) { }

        public Manager? GetByName(string name)
        {
            return Items.Find(manager => manager.Name == name);
        }
    }

}