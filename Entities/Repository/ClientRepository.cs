using Domain.Entities;
using Domain.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class ClientRepository : BaseRepository<Client>
    {
        public ClientRepository(string filePath) : base(filePath) { }
        public Client GetById(int clientId)
        {
            return Items.FirstOrDefault(client => client.ClientId == clientId);
        }
        public Client GetByPhoneNumber(string phoneNumber)
        {
            return Items.FirstOrDefault(client => client.PhoneNumber == phoneNumber);
        }
    }

}