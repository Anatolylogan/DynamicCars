using Domain.Entities;
using Domain.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class ClientRepository
    {
        private readonly List<Client> clients = new List<Client>();
        private readonly IdGenerator _idGenerator;

        public ClientRepository(IdGenerator idGenerator)
        {
            _idGenerator = idGenerator;
        }

        public void Add(Client client)
        {
            client.ClientId = _idGenerator.GenerateId();  
            clients.Add(client);
        }

        public Client GetById(int id)
        {
            return clients.FirstOrDefault(c => c.ClientId == id);
        }

        public List<Client> GetAll()
        {
            return clients;
        }

        public Client GetByName(string name)
        {
            return clients.FirstOrDefault(client => client.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}