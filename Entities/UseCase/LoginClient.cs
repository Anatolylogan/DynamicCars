using Domain.Entities;
using Infrastructure.Repository;

namespace Domain.UseCase
{
    public class LoginClientUseCase
    {
        private readonly ClientRepository _clientRepository;

        public LoginClientUseCase(ClientRepository clientRepository)
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