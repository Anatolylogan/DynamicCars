using Infrastructure.Repository;
using Application.UseCase;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var clientRepository = new ClientRepository("clients.json");
            var makerRepository = new MakerRepository("makers.json");
            var orderRepository = new OrderRepository("orders.json");
            var storeRepository = new StoreRepository("store.json");
            var deliveryRepository = new DeliveryRepository("delivery.json");
            var pricingRepository = new PricingRepository("pricing.json");
            var managerRepository = new ManagerRepository("manager.json");

            var calculateOrderCostUseCase = new CalculateOrderCostUseCase(pricingRepository);
            var filterOrdersByStatusUseCase = new FilterOrdersByStatusUseCase(orderRepository);
            var notificationService = new NotificationService();
            var loginClientUseCase = new LoginClientUseCase(clientRepository);
            var assignMakerToOrderUseCase = new AssignMakerToOrderUseCase(orderRepository, makerRepository);
            var cancelOrderUseCase = new CancelOrderUseCase(orderRepository);
            var sendToStoreUseCase = new SendToStoreUseCase(orderRepository, storeRepository);
            var sendToDeliveryUseCase = new SendToDeliveryUseCase(orderRepository, deliveryRepository);
            var managerService = new ManagerService(orderRepository, makerRepository, managerRepository);
            var registerManagerUseCase = new RegisterManagerUseCase(managerRepository);
            var completeMakingUseCase = new CompleteMakingUseCase(orderRepository)
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
            int maxOrderId = orderRepository.GetAll().Any() ? orderRepository.GetAll().Max(o => o.Id) : 0;
            int maxClientId = clientRepository.GetAll().Any() ? clientRepository.GetAll().Max(c => c.Id) : 0;
            int startId = Math.Max(maxOrderId, maxClientId);
            IdGenerator idGenerator = new IdGenerator(startId);
            var createOrderUseCase = new CreateOrderUseCase(orderRepository, clientRepository, idGenerator)
            {
                Logger = Logger.LogToConsole
            };
            var registerClientUseCase = new RegisterClientUseCase(clientRepository, idGenerator);
            


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
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowClientMenu(clientRepository, createOrderUseCase, loginClientUseCase, cancelOrderUseCase, registerClientUseCase, calculateOrderCostUseCase, orderRepository);
                        break;
                    case "2":
                        ShowManagerMenu(managerRepository, createOrderUseCase, assignMakerToOrderUseCase, completeMakingUseCase, sendToStoreUseCase, sendToDeliveryUseCase, registerManagerUseCase, orderRepository, filterOrdersByStatusUseCase, clientRepository);
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

        static void ShowClientMenu(ClientRepository clientRepository, CreateOrderUseCase createOrderUseCase, LoginClientUseCase loginClientUseCase, CancelOrderUseCase cancelOrderUseCase, RegisterClientUseCase registerClientUseCase, CalculateOrderCostUseCase orderCostCalculator, OrderRepository orderRepository)
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
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterClient(registerClientUseCase);
                        break;
                    case "2":
                        LoginClient(loginClientUseCase);
                        break;
                    case "3":
                        MakeOrder(createOrderUseCase, clientRepository);
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
            string? clientName = Console.ReadLine();
            Console.WriteLine("Ввидеите номер телефона(Пример<+7(999)-999-99-99>)");
            string? phoneNumber = Console.ReadLine();
            var client = registerClientUseCase.Execute(clientName, phoneNumber);
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
        static void MakeOrder(CreateOrderUseCase createOrderUseCase, ClientRepository clientRepository)
        {
            Console.Write("Введите ID клиента для заказа: ");
            int orderClientId = int.Parse(Console.ReadLine());
            Console.Write("Введите марку автомобиля: ");
            string carBrand = Console.ReadLine();
            Console.Write("Введите цвет ковров: ");
            string carpetColor = Console.ReadLine();
            Console.WriteLine("Выберите способ доставки:");
            Console.WriteLine("1. Стандартная доставка");
            Console.WriteLine("2. Экспресс доставка");
            Console.WriteLine("3. Доставка на следующий день");
            string deliveryChoice = Console.ReadLine();
            IDeliveryOption deliveryOption = deliveryChoice switch
            {
                "1" => new StandardDelivery(),
                "2" => new ExpressDelivery(),
                "3" => new NextDayDelivery(),
                _ => throw new Exception("Неверный выбор способа доставки.")
            };
            createOrderUseCase.Execute(orderClientId, new List<(string color, string carBrand)>
    {
        (carpetColor, carBrand)
    }, deliveryOption);
            Console.WriteLine($"Заказ создан с маркой автомобиля {carBrand}, цветом ковров {carpetColor}.");
            Console.WriteLine($"Способ доставки: {deliveryOption.GetDeliveryDetails()}");
            Console.WriteLine($"Стоимость доставки: {deliveryOption.GetCost()}");
            Console.ReadKey();
        }
        static void CalculateOrderCost(CalculateOrderCostUseCase costCalculatorUseCase, OrderRepository orderRepository)
        {
            Console.Write("Введите ID заказа для расчета стоимости: ");
            int orderId = int.Parse(Console.ReadLine());
            var order = orderRepository.GetById(orderId);

            if (order != null)
            {
                if (order.Items == null || !order.Items.Any())
                {
                    Console.WriteLine("Заказ не содержит товаров. Невозможно рассчитать стоимость.");
                }
                else
                {
                    Console.WriteLine("Выберите тип расчета:");
                    Console.WriteLine("1. Без скидки");
                    Console.WriteLine("2. С учетом скидки для постоянных клиентов");
                    string choice = Console.ReadLine();

                    string calculatorType = choice == "1" ? "basic" : "discounted";

                    try
                    {
                        decimal cost = costCalculatorUseCase.Execute(order, calculatorType);
                        Console.WriteLine($"Стоимость заказа ID {orderId}: {cost}.");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                }
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

        static void ShowManagerMenu(ManagerRepository managerRepository, CreateOrderUseCase createOrderUseCase, AssignMakerToOrderUseCase assignMakerToOrderUseCase, CompleteMakingUseCase completeMakingUseCase, SendToStoreUseCase sendToStoreUseCase, SendToDeliveryUseCase sendToDeliveryUseCase, RegisterManagerUseCase registerManagerUseCase, OrderRepository orderRepository, FilterOrdersByStatusUseCase filterOrdersByStatusUseCase, ClientRepository clientRepository)
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
                        ListOrders(orderRepository, clientRepository);
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

        static void ListOrders(OrderRepository orderRepository, ClientRepository clientRepository)
        {
            var orders = orderRepository.GetAll();

            if (!orders.Any())
            {
                Console.WriteLine("Список заказов пуст.");
                return;
            }

            Console.WriteLine("Список заказов:");
            Console.WriteLine();
            foreach (var order in orders)
            {
                var client = clientRepository.GetById(order.ClientId);
                var Client = clientRepository.GetByPhoneNumber(client.PhoneNumber);

                string clientName = client != null ? client.Name : "Неизвестный клиент";
                string phoneNumber = client != null ? client.PhoneNumber : "Неизвестный номер телефона";

                Console.WriteLine($"ID Заказа: {order.Id}");
                Console.WriteLine($"ID Клиента: {order.ClientId}");
                Console.WriteLine($"Имя Клиента: {clientName}");
                Console.WriteLine($"Контактный номер телефона: {phoneNumber}");
                Console.WriteLine($"Марка Автомобиля: {order.Items.FirstOrDefault()?.CarBrand ?? "Не задан"}");
                Console.WriteLine($"Цвет Ковров: {order.Items.FirstOrDefault()?.CarpetColor ?? "Не задан"}");
                Console.WriteLine($"Способ доставки: {order.DeliveryDetails}");
                Console.WriteLine($"Стоимость доставки: {order.DeliveryCost}");
                Console.WriteLine($"Статус Заказа: {order.Status}");
                Console.WriteLine("");
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
