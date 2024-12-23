using Domain.Entities;
using Infrastructure.Repository;

namespace Domain.UseCase
{
    public class RegisterClientUseCase
    {
        private readonly ClientRepository _clientRepository;
        private readonly IdGenerator _idGenerator;

        public RegisterClientUseCase(ClientRepository clientRepository, IdGenerator idGenerator)
        {
            _clientRepository = clientRepository;
            _idGenerator = idGenerator;
        }

        public Client Execute(string clientName)
        {
            int newClientId = _idGenerator.GenerateId();
            if (string.IsNullOrWhiteSpace(clientName))
            {
                throw new ArgumentException("Имя клиента не может быть пустым.");
            }

            var client = new Client { Id = newClientId , Name = clientName };
            _clientRepository.Add(client);

            return client;
        }
    }
}