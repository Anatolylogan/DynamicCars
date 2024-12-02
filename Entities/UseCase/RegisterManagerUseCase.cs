using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Domain.Entities;
using Domain.Repository;

namespace Domain.UseCase
{
    public class RegisterManagerUseCase
    {
        private readonly IManagerRepository _managerRepository;
        public RegisterManagerUseCase(ManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        public Manager Execute(string managerName)
        {
          
            if (string.IsNullOrEmpty(managerName))
            {
                throw new ArgumentException("Имя менеджера не может быть пустым.");
            }

            if (_managerRepository.GetByName(managerName) != null)
            {
                throw new InvalidOperationException("Менеджер с таким именем уже существует.");
            }

            var manager = new Manager();
            return _managerRepository.Add(manager);
        }
    }
}
