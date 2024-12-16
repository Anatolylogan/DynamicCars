using Domain.Entities;

namespace Infrastructure.Repository
{
    public class ClientRepository : BaseRepository<Client>
    {
        public ClientRepository(string filePath) : base(filePath) { }
        public Client? GetByPhoneNumber(string phoneNumber)
        {
            return Items.FirstOrDefault(client => client.PhoneNumber == phoneNumber);
        }
    }

}