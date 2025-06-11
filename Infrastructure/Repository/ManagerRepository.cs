using Application.Entities;
using Application.Сontracts;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repository
{
    public class ManagerRepository : BaseRepository<Manager>, IManagerRepository
    {
        public ManagerRepository(IOptions<RepositorySettings> options)
            : base(options.Value.ManagersFilePath) { }
        public ManagerRepository(string filePath) : base(filePath) { }

        public Manager? GetByName(string name)
        {
            return Items.Find(manager => manager.Name == name);
        }
    }

}