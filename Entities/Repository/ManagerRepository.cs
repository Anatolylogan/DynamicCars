using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    namespace Domain.Repository
    {
        public interface IManagerRepository
        {
            Manager Add(Manager manager);
            Manager GetByName(string name);
            List<Manager> GetAll();
        }

        public class ManagerRepository : IManagerRepository
        {
            private readonly List<Manager> _managers = new List<Manager>();
            private static int _nextId = 1;

            public Manager Add(Manager manager)
            {
                manager.ManagerId = _nextId++;
                _managers.Add(manager);
                return manager;
            }

            public Manager GetByName(string name)
            {
                return _managers.Find(m => m.Name == name);
            }

            public List<Manager> GetAll()
            {
                return _managers;
            }
        }
    }