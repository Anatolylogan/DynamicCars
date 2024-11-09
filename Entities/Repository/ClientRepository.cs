using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class ClientRepository
    {
        private readonly List<Client> clients = new List<Client> ();
        public void Add(Client client) //Добавление клиента
        {
            clients.Add (client);
        }
        public Client GetById(int id) //Получение клинт по ид
        {
            return clients.FirstOrDefault(c => c.ClientId == id);
        }
        public List<Client> GetAll()// Все клиенты
        {
            return clients;
        }

    }  
}
