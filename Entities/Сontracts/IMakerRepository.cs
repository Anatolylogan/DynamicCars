using Domain.Entities;

namespace Domain.Сontracts
{
    public interface IMakerRepository : IRepository<Maker>
    {
        Maker GetByName(string name);
    }
}