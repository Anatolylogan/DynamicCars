using Domain.Entities;
using Domain.Сontracts;

namespace Infrastructure.Repository
{
    public class ManagerRepository : BaseRepository<Manager>, IManagerRepository
    {
        public ManagerRepository(string filePath) : base(filePath) { }

        public Manager? GetByName(string name)
        {
            return Items.Find(manager => manager.Name == name);
        }
    }

}