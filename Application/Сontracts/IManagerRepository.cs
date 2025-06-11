using Application.Entities;

namespace Application.Сontracts
{
    public interface IManagerRepository : IRepository<Manager>
    {
        Manager? GetByName(string name);
    }
}