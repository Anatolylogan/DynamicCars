using Application.Entities;
using Application.Сontracts;

namespace Application.UseCase
{
    public class LoginManagerUseCase
    {
        private readonly IManagerRepository _managerRepository;

        public LoginManagerUseCase(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        public Manager Execute(string managerName)
        {
            if (string.IsNullOrWhiteSpace(managerName))
                throw new ArgumentException("Имя менеджера не может быть пустым.");

            var manager = _managerRepository.GetByName(managerName);
            if (manager == null)
                throw new InvalidOperationException("Менеджер не найден.");

            return manager;
        }
    }
}
