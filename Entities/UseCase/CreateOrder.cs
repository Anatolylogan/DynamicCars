using Domain.Entities;
using Domain.UseCase;
using Infrastructure.Repository;

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

        var order = new Order
        {
            Id = _idGenerator.GenerateId(),
            ClientId = clientId,
            Status = OrderStatus.New,
            Items = matChoices.Select(choice => new OrderItem
            {
                CarpetColor = choice.color,
                CarBrand = choice.carBrand
            }).ToList()
        };

        _orderRepository.Add(order);

        Logger?.Invoke($"Создан заказ: ID {order.Id}, клиент {clientId}, товаров {order.Items.Count}");
    }
}
