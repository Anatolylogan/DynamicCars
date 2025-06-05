using Domain.Entities;

namespace Domain.Сontracts
{
    public interface IClientRepository : IRepository<Client>
    {
        Client? GetByPhoneNumber(string phoneNumber);
    }
}