using Application.Entities;
using Application.Сontracts;

namespace Application.UseCase
{
    public class LoginClientUseCase
    {
        private readonly IClientRepository _clientRepository;

        public LoginClientUseCase(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Client Execute(int clientId)
        {
            var client = _clientRepository.GetById(clientId);
            if (client == null)
            {
                throw new Exception("Клиент не найден");
            }
            return client;
        }
    }
}