using Domain.Entities;
using Domain.Repository;

    namespace Domain.UseCase
    {
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
                    Console.WriteLine("Клиент с таким ID не найден.");
                    return;
                }

               
                var order = new Order
                {
                    OrderID = _idGenerator.GenerateId(),
                    ClientId = client.ClientId,
                    Client = client,
                    Status = "Создан", 
                    MatChoices = matChoices
                };

                foreach (var matChoice in matChoices)
                {
                    var mat = new Mat
                    {
                        Color = matChoice.color,
                        CarBrand = matChoice.carBrand
                    };
                    order.Mats.Add(mat);
                }

       
                _orderRepository.Add(order);

                Console.WriteLine($"Заказ с ID {order.OrderID} успешно создан для клиента {client.Name}.");
            }
        }
    }
