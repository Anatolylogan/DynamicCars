using Domain.Entities;
using Infrastructure.Repository;
using Domain.UseCase;

public class CreateOrderUseCase
{
    private readonly OrderRepository _orderRepository;
    private readonly ClientRepository _clientRepository;
    private readonly IdGenerator _idGenerator;
    public LogHandler Logger { get; set; } 
    public CreateOrderUseCase(OrderRepository orderRepository, ClientRepository clientRepository, IdGenerator idGenerator)
    {
        _orderRepository = orderRepository;
        _clientRepository = clientRepository;
        _idGenerator = idGenerator;
    }

    public void Execute(int clientId, List<(string color, string carBrand)> matChoices)
    {
        var client = _clientRepository.GetById(clientId);
        if (client == null)
        {
            Logger?.Invoke($"Ошибка: клиент с ID {clientId} не найден.");
            throw new Exception("Клиент не найден.");
        }

        foreach (var choice in matChoices)
        {
            var order = new Order
            {
                Id = _idGenerator.GenerateId(),
                ClientId = clientId,
                Status = OrderStatus.New,
                CarBrand = choice.carBrand,
                CarpetColor = choice.color
            };
            _orderRepository.Add(order);

            Logger?.Invoke($"Создан заказ: ID {order.Id}, клиент {clientId}, автомобиль {choice.carBrand}, цвет {choice.color}");
        }
    }
}
