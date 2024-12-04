using Domain.Entities;
using Domain.Repository;
using Domain.UseCase;

public class CreateOrderUseCase
{
    private readonly OrderRepository _orderRepository;
    private readonly ClientRepository _clientRepository;
    private readonly IdGenerator _idGenerator;

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
            throw new Exception("Клиент не найден");
        }

        var order = new Order
        {
            OrderID = _idGenerator.GenerateId(),
            ClientId = client.ClientId,
            Status = "Создан",
            Mats = matChoices.Select(choice => new Mat
            {
                Color = choice.color,
                CarBrand = choice.carBrand
            }).ToList()
        };

        _orderRepository.Add(order);
        Console.WriteLine($"Заказ с ID {order.OrderID} успешно создан для клиента {client.Name}.");
    }
}
