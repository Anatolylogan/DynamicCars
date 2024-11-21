using Domain.Entities;
using Domain.Repository;

namespace Domain.UseCase
{
    public class CreateOrderUseCase
    {
        private readonly OrderRepository _orderRepository;
        private readonly ClientRepository _clientRepository;

        public CreateOrderUseCase(OrderRepository orderRepository, ClientRepository clientRepository)
        {
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
        }

        public Order Execute(int clientId, List<(string color, string carBrand)> matChoices)
        {
            var client = _clientRepository.GetById(clientId);
            if (client == null)
            {
                throw new Exception("Клиент не найден");
            }
            var mats = matChoices.Select(choice => new Mat
            {
                Color = choice.color,
                CarBrand = choice.carBrand
            }).ToList();

            var order = new Order
            {
                Client = client,
                ClientId = client.ClientId,
                Mats = mats,
                Status = "Создан"
            };

            _orderRepository.Add(order);
            return order;
        }
    }
}