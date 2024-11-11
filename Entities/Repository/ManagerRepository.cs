using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class ManagerRepository
    {
        private List<Manager> managers = new List<Manager>();


        public void Add(Manager manager)
        {
            managers.Add(manager);
        }
        public Manager GetByName(string name)
        {
            return managers.FirstOrDefault(manager => manager.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
        public List<Manager> GetAll()
        {
            return managers;
        }
    }

}
