using Domain.Entities;
using Domain.Сontracts;

namespace Infrastructure.Repository
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(string filePath) : base(filePath) { }
        public Client? GetByPhoneNumber(string phoneNumber)
        {
            return Items.FirstOrDefault(client => client.PhoneNumber == phoneNumber);
        }
    }

}