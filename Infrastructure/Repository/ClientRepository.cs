using Domain.Entities;
using Domain.Сontracts;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repository
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(IOptions<RepositorySettings> options)
          : base(options.Value.ClientsFilePath) { }
        public ClientRepository(string filePath) : base(filePath) { }
        public Client? GetByPhoneNumber(string phoneNumber)
        {
            return Items.FirstOrDefault(client => client.PhoneNumber == phoneNumber);
        }
    }

}