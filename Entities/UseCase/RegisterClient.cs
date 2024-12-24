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

        public Client Execute(string clientName, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(clientName))
            {
                throw new ArgumentException("Имя клиента не может быть пустым.");
            }

            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentException("Номер телефона не может быть пустым.");
            }

            int newClientId = _idGenerator.GenerateId();

            var client = new Client
            {
                Id = newClientId,
                Name = clientName,
                PhoneNumber = phoneNumber
            };

            _clientRepository.Add(client);

            return client;
        }
    }
}
