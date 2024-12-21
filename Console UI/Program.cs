using Infrastructure.Repository;
using Domain.UseCase;
using Domain.UseCase.Domain.UseCase;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string ordersFilePath = "orders.json";
            string clientsFilePath = "clients.json";
            string makersFilePath = "makers.json";
            string deliveriesFilePath = "deliveries.json";
            string managersFilePath = "managers.json";

            IdGenerator idGenerator = new IdGenerator();

            var clientRepository = new ClientRepository(clientsFilePath);
            var makerRepository = new MakerRepository(makersFilePath);
            var orderRepository = new OrderRepository(ordersFilePath);
            var storeRepository = new StoreRepository();
            var deliveryRepository = new DeliveryRepository(deliveriesFilePath);

            var filterOrdersByStatusUseCase = new FilterOrdersByStatusUseCase(orderRepository);
            var notificationService = new NotificationService();
            var managerRepository = new ManagerRepository(managersFilePath);
            var registerClientUseCase = new RegisterClientUseCase(clientRepository);
            var loginClientUseCase = new LoginClientUseCase(clientRepository);
            var assignMakerToOrderUseCase = new AssignMakerToOrderUseCase(orderRepository, makerRepository);
            var cancelOrderUseCase = new CancelOrderUseCase(orderRepository);
            var sendToStoreUseCase = new SendToStoreUseCase(orderRepository, storeRepository);
            var sendToDeliveryUseCase = new SendToDeliveryUseCase(orderRepository, deliveryRepository);
            var managerService = new ManagerService(orderRepository, makerRepository, managerRepository);
            var registerManagerUseCase = new RegisterManagerUseCase(managerRepository);
            var createOrderUseCase = new CreateOrderUseCase(orderRepository, clientRepository, idGenerator)
            {
                Logger = Logger.LogToConsole
            };
            var completeMakingUseCase = new CompleteMakingUseCase(orderRepository)
            {
                Logger = Logger.LogToConsole
            };
            var orderCostCalculatorUse = new OrderCostCalculatorUseCase
            {
                Logger = Logger.LogToConsole
            };
            completeMakingUseCase.OrderReady += order =>
            {
                Console.WriteLine($"Уведомление: Заказ с ID {order.Id} готов для клиента {order.ClientEmail}.");
            };

            completeMakingUseCase.OrderReady += order =>
            {
                Console.WriteLine($"Отправка email на адрес {order.ClientEmail}: Ваш заказ с ID {order.Id} готов.");
            };



            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Добро пожаловать в Dynamic Cars");
                Console.WriteLine("Главное меню:");
                Console.WriteLine("1. Меню клиента");
                Console.WriteLine("2. Меню менеджера");
                Console.WriteLine("3. Выход");
                Console.Write("Выберите опцию: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowClientMenu(clientRepository, createOrderUseCase, loginClientUseCase, cancelOrderUseCase, registerClientUseCase, orderCostCalculatorUse, orderRepository);
                        break;
                    case "2":
                        ShowManagerMenu(managerRepository, createOrderUseCase, assignMakerToOrderUseCase, completeMakingUseCase, sendToStoreUseCase, sendToDeliveryUseCase, registerManagerUseCase, orderRepository, filterOrdersByStatusUseCase);
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static void ShowClientMenu(ClientRepository clientRepository, CreateOrderUseCase createOrderUseCase, LoginClientUseCase loginClientUseCase, CancelOrderUseCase cancelOrderUseCase, RegisterClientUseCase registerClientUseCase, OrderCostCalculatorUseCase orderCostCalculator, OrderRepository orderRepository)
        {
            bool exitClientMenu = false;

            while (!exitClientMenu)
            {
                Console.Clear();
                Console.WriteLine("Меню клиента:");
                Console.WriteLine("1. Зарегистрироваться");
                Console.WriteLine("2. Войти");
                Console.WriteLine("3. Сделать заказ");
                Console.WriteLine("4. Рассчитать стоимость заказа");
                Console.WriteLine("5. Отменить заказ");
                Console.WriteLine("6. Назад");
                Console.Write("Выберите опцию: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterClient(registerClientUseCase);
                        break;
                    case "2":
                        LoginClient(loginClientUseCase);
                        break;
                    case "3":
                        MakeOrder(createOrderUseCase);
                        break;
                    case "4":
                        CalculateOrderCost(orderCostCalculator, orderRepository);
                        break;
                    case "5":
                        CancelOrder(cancelOrderUseCase);
                        break;
                    case "6":
                        exitClientMenu = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static void RegisterClient(RegisterClientUseCase registerClientUseCase)
        {
            Console.Write("Введите имя клиента: ");
            string clientName = Console.ReadLine();
            var client = registerClientUseCase.Execute(clientName);
            Console.WriteLine($"Клиент {client.Name} успешно зарегистрирован с ID {client.Id}");
            Console.ReadKey();
        }

        static void LoginClient(LoginClientUseCase loginClientUseCase)
        {
            Console.Write("Введите ID клиента: ");
            int clientId = int.Parse(Console.ReadLine());
            var loggedInClient = loginClientUseCase.Execute(clientId);
            if (loggedInClient != null)
            {
                Console.WriteLine($"Клиент с ID {clientId} вошел.");
            }
            else
            {
                Console.WriteLine("Клиент не найден.");
            }
            Console.ReadKey();
        }

        static void MakeOrder(CreateOrderUseCase createOrderUseCase)
        {
            Console.Write("Введите ID клиента для заказа: ");
            int orderClientId = int.Parse(Console.ReadLine());
            Console.Write("Введите марку автомобиля: ");
            string carBrand = Console.ReadLine();
            Console.Write("Введите цвет ковров: ");
            string carpetColor = Console.ReadLine();
            createOrderUseCase.Execute(orderClientId, new List<(string color, string carBrand)>
            {
                (carpetColor, carBrand)
            });

            Console.WriteLine($"Заказ создан с маркой автомобиля {carBrand} и цветом ковров {carpetColor}.");
            Console.ReadKey();
        }
        static void CalculateOrderCost(OrderCostCalculatorUseCase costCalculator, OrderRepository orderRepository)
        {
            Console.Write("Введите ID заказа для расчета стоимости: ");
            int orderId = int.Parse(Console.ReadLine());
            var order = orderRepository.GetById(orderId);
            if (order != null)
            {
                decimal cost = costCalculator.CalculateCost(order);
                Console.WriteLine($"Стоимость заказа ID {orderId}: {cost}.");
            }
            else
            {
                Console.WriteLine("Заказ не найден.");
            }
            Console.ReadKey();
        }

        static void CancelOrder(CancelOrderUseCase cancelOrderUseCase)
        {
            Console.Write("Введите ID заказа для отмены: ");
            int cancelOrderId = int.Parse(Console.ReadLine());
            Console.Write("Введите ID клиента для подтверждения отмены: ");
            int cancelClientId = int.Parse(Console.ReadLine());
            cancelOrderUseCase.Execute(cancelOrderId, cancelClientId);
            Console.WriteLine($"Заказ с ID {cancelOrderId} отменен.");
            Console.ReadKey();
        }

        static void ShowManagerMenu(ManagerRepository managerRepository, CreateOrderUseCase createOrderUseCase, AssignMakerToOrderUseCase assignMakerToOrderUseCase, CompleteMakingUseCase completeMakingUseCase, SendToStoreUseCase sendToStoreUseCase, SendToDeliveryUseCase sendToDeliveryUseCase, RegisterManagerUseCase registerManagerUseCase, OrderRepository orderRepository, FilterOrdersByStatusUseCase filterOrdersByStatusUseCase)
        {
            bool exitManagerMenu = false;

            while (!exitManagerMenu)
            {
                Console.Clear();
                Console.WriteLine("Меню менеджера:");
                Console.WriteLine("1. Зарегистрироваться как менеджер");
                Console.WriteLine("2. Войти как менеджер");
                Console.WriteLine("3. Список заказов");
                Console.WriteLine("4. Назначить заказ на изготовителя");
                Console.WriteLine("5. Изменить статус заказа на выполнено");
                Console.WriteLine("6. Отправить заказ на склад");
                Console.WriteLine("7. Отправить заказ на доставку");
                Console.WriteLine("8. Отобразить заказы по статусу");
                Console.WriteLine("9. Назад");
                Console.Write("Выберите опцию: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterManager(registerManagerUseCase);
                        break;
                    case "2":
                        LoginManager(managerRepository);
                        break;
                    case "3":
                        ListOrders(orderRepository);
                        break;
                    case "4":
                        AssignOrderToMaker(assignMakerToOrderUseCase);
                        break;
                    case "5":
                        CompleteOrder(completeMakingUseCase, orderRepository);
                        break;
                    case "6":
                        SendToStore(sendToStoreUseCase);
                        break;
                    case "7":
                        SendToDelivery(sendToDeliveryUseCase);
                        break;
                    case "8":
                        FilterOrders(orderRepository, filterOrdersByStatusUseCase);
                        break;
                    case "9":
                        exitManagerMenu = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static void RegisterManager(RegisterManagerUseCase registerManagerUseCase)
        {
            Console.Write("Введите имя менеджера: ");
            string managerName = Console.ReadLine();
            var manager = registerManagerUseCase.Execute(managerName);
            Console.WriteLine($"Менеджер {manager.Name} успешно зарегистрирован с ID {manager.Id}");
            Console.ReadKey();
        }

        static void LoginManager(ManagerRepository managerRepository)
        {
            Console.Write("Введите Имя менеджера: ");
            string Name = Console.ReadLine();
            var manager = managerRepository.GetByName(Name);
            if (manager != null)
            {
                Console.WriteLine($"Менеджер {Name} вошёл в систему.");
            }
            else
            {
                Console.WriteLine("Менеджер не найден.");
            }
            Console.ReadKey();
        }

        static void ListOrders(OrderRepository orderRepository)
        {
            var orders = orderRepository.GetAll();
            foreach (var order in orders)
            {
                Console.WriteLine($"ID заказа: {order.Id}, ID Клиента : {order.Id}, Статус заказа: {order.Status}");
            }
            Console.ReadKey();
        }

        static void AssignOrderToMaker(AssignMakerToOrderUseCase assignMakerToOrderUseCase)
        {
            Console.Write("Введите ID заказа для назначения изготовителя: ");
            int orderId = int.Parse(Console.ReadLine());
            Console.Write("Введите Имя изготовителя: ");
            string Name = Console.ReadLine();
            assignMakerToOrderUseCase.Execute(orderId, Name);
            Console.WriteLine($"Заказ с ID {orderId} назначен на изготовителя {Name}");
            Console.ReadKey();
        }

        static void CompleteOrder(CompleteMakingUseCase completeMakingUseCase, OrderRepository orderRepository)
        {
            Console.Write("Введите ID заказа для завершения: ");
            int orderId = int.Parse(Console.ReadLine());

            Console.Write("Введите Email клиента: ");
            string clientEmail = Console.ReadLine();

            try
            {
                completeMakingUseCase.Execute(orderId, clientEmail);
                Console.WriteLine($"Заказ с ID {orderId} успешно завершен.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.ReadKey();
        }



        static void SendToStore(SendToStoreUseCase sendToStoreUseCase)
        {
            Console.Write("Введите ID заказа для отправки на склад: ");
            int orderId = int.Parse(Console.ReadLine());
            sendToStoreUseCase.Execute(orderId);
            Console.WriteLine($"Заказ с ID {orderId} отправлен на склад.");
            Console.ReadKey();
        }

        static void SendToDelivery(SendToDeliveryUseCase sendToDeliveryUseCase)
        {
            Console.Write("Введите ID заказа для отправки на доставку: ");
            int orderId = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите адрес для доставки:");
            var address = Console.ReadLine();
            sendToDeliveryUseCase.Execute(orderId, address);
            Console.WriteLine($"Заказ с ID {orderId} отправлен на доставку по адресу {address}");
            Console.ReadKey();
        }
        static void FilterOrders(OrderRepository orderRepository, FilterOrdersByStatusUseCase filterOrdersByStatusUseCase)
        {
            Console.WriteLine("Введите статус для фильтрации заказов.(Статусы:'New','InProgress','Completed','Warehouse','OnDelivery','Canceled'");
            string statusInput = Console.ReadLine();

            if (Enum.TryParse(statusInput, true, out OrderStatus status))
            {
                var filteredOrders = filterOrdersByStatusUseCase.Execute(status);

                if (filteredOrders.Any())
                {
                    Console.WriteLine("Найденные заказы:");
                    foreach (var order in filteredOrders)
                    {
                        Console.WriteLine($"ID: {order.Id}, Клиент ID: {order.ClientId}, Статус: {order.Status}");
                    }
                }
                else
                {
                    Console.WriteLine("Нет заказов с указанным статусом.");
                }
            }
            else
            {
                Console.WriteLine("Неверный статус. Попробуйте снова.");
            }
            Console.ReadKey();
        }
    }
}
