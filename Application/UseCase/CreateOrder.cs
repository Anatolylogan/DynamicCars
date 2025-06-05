using Domain.Entities;
using Domain.UseCase;
using Domain.Сontracts;
using System.Collections.Generic;

public class CreateOrderUseCase
{
    private readonly IdGenerator _idGenerator;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Client> _clientRepository;

    public LogHandler Logger { get; set; }

    public CreateOrderUseCase(IOrderRepository orderRepository, IClientRepository clientRepository, IdGenerator idGenerator)
    {
        _orderRepository = orderRepository;
        _clientRepository = clientRepository;
        _idGenerator = idGenerator;
    }

    public List<Order> Execute(int clientId, List<(string color, string carBrand)> matChoices, IDeliveryOption deliveryOption)
    {
        var client = _clientRepository.GetById(clientId);
        if (client == null)
        {
            Logger?.Invoke($"Ошибка: клиент с ID {clientId} не найден.");
            throw new KeyNotFoundException("Клиент не найден.");
        }

        var createdOrders = new List<Order>();

        foreach (var choice in matChoices)
        {
            var order = new Order
            {
                Id = _idGenerator.GenerateId(),
                ClientId = clientId,
                Status = OrderStatus.New,
                CarBrand = choice.carBrand,
                CarpetColor = choice.color,
                DeliveryDetails = deliveryOption.GetDeliveryDetails(),
                DeliveryCost = deliveryOption.GetCost()
            };
            order.Items.Add(new OrderItem
            {
                CarBrand = choice.carBrand,
                CarpetColor = choice.color,
            });

            _orderRepository.Add(order);

            Logger?.Invoke($"Создан заказ: ID {order.Id}, клиент {clientId}, автомобиль {choice.carBrand}, цвет {choice.color}");
            Logger?.Invoke($"Способ доставки: {order.DeliveryDetails}, Стоимость доставки: {order.DeliveryCost}");

            createdOrders.Add(order);
        }

        return createdOrders;
    }
}
