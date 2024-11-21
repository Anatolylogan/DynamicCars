using Domain.Entities;
using Domain.Repository;

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
            return _clientRepository.GetById(clientId);
        }
    }
}