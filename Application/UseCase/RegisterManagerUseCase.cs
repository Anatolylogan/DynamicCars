using Application.Entities;
using Application.Сontracts;

namespace Application.UseCase
{
    public class RegisterManagerUseCase
    {
        private readonly IManagerRepository _managerRepository;

        public RegisterManagerUseCase(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        public Manager Execute(string managerName)
        {
            if (string.IsNullOrWhiteSpace(managerName))
            {
                throw new ArgumentException("Имя менеджера не может быть пустым.");
            }

            if (_managerRepository.GetByName(managerName) != null)
            {
                throw new InvalidOperationException("Менеджер с таким именем уже существует.");
            }

            var manager = new Manager { Name = managerName };
            _managerRepository.Add(manager);

            return manager;
        }
    }
}