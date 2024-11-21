using Domain.Entities;
using Domain.Repository;

namespace Domain.UseCase
{
    public class RegisterClientUseCase
    {
        private readonly ClientRepository _clientRepository;

        public RegisterClientUseCase(ClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Client Execute(string clientName)
        {
            var client = new Client
            {
                Name = clientName
            };

            _clientRepository.Add(client);
            return client;
        }
    }
}
