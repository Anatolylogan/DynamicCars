using System.Net;
using Domain.Entities;
using Domain.Сontracts;

namespace Domain.UseCase
{
    public class RegisterClientUseCase
    {
        private readonly IdGenerator _idGenerator;
        private readonly IClientRepository _clientRepository;

        public RegisterClientUseCase(IClientRepository clientRepository, IdGenerator idGenerator)
        {
            _clientRepository = clientRepository;
            _idGenerator = idGenerator;
        }
        public Client? Execute(string clientName, string phoneNumber)
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

            var newClient = new Client
            {
                Id = newClientId,
                Name = clientName,
                PhoneNumber = phoneNumber
            };

            _clientRepository.Add(newClient);

            return newClient;
        }
    }
}
