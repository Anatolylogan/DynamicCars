using Application.Entities;

namespace Application.Сontracts
{
    public interface IMakerRepository : IRepository<Maker>
    {
        Maker GetByName(string name);
    }
}