using Domain.Entities;
using Infrastructure.Repository;

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
            if (string.IsNullOrWhiteSpace(clientName))
            {
                throw new ArgumentException("Имя клиента не может быть пустым.");
            }

            var client = new Client { Name = clientName };
            _clientRepository.Add(client);

            return client;
        }
    }
}