using Application.Entities;

namespace Application.Сontracts
{
    public interface IClientRepository : IRepository<Client>
    {
        Client? GetByPhoneNumber(string phoneNumber);
    }
}