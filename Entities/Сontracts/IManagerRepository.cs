using Domain.Entities;

namespace Domain.Сontracts
{
    public interface IManagerRepository : IRepository<Manager>
    {
        Manager? GetByName(string name);
    }
}