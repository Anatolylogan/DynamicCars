
using Domain.Entities;
using Domain.Repository;
using Domain.UseCase;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IdGenerator idGenerator = new IdGenerator();

            var clientRepository = new ClientRepository(idGenerator);
            var makerRepository = new MakerRepository();
            var orderRepository = new OrderRepository();
            var storeRepository = new StoreRepository();
            var deliveryRepository = new DeliveryRepository();
            var managerRepository = new ManagerRepository();

            var createOrderUseCase = new CreateOrderUseCase(orderRepository, clientRepository, idGenerator);
            var registerClientUseCase = new RegisterClientUseCase(clientRepository);
            var loginClientUseCase = new LoginClientUseCase(clientRepository);
            var assignMakerToOrderUseCase = new AssignMakerToOrderUseCase(orderRepository, makerRepository);
            var cancelOrderUseCase = new CancelOrderUseCase(orderRepository);
            var completeMakingUseCase = new CompleteMakingUseCase(orderRepository);
            var deliveryService = new DeliveryService(deliveryRepository, orderRepository);
            var sendToStoreUseCase = new SendToStoreUseCase(orderRepository, storeRepository);
            var sendToDeliveryUseCase = new SendToDeliveryUseCase(orderRepository, deliveryRepository);
            var managerService = new ManagerService(orderRepository, makerRepository, managerRepository);
            var registerManagerUseCase = new RegisterManagerUseCase(managerRepository);

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
                        ShowClientMenu(clientRepository, createOrderUseCase, loginClientUseCase, cancelOrderUseCase, registerClientUseCase);
                        break;
                    case "2":
                        ShowManagerMenu(managerRepository, createOrderUseCase, assignMakerToOrderUseCase, completeMakingUseCase, sendToStoreUseCase, sendToDeliveryUseCase, registerManagerUseCase, orderRepository);
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

        static void ShowClientMenu(ClientRepository clientRepository, CreateOrderUseCase createOrderUseCase, LoginClientUseCase loginClientUseCase, CancelOrderUseCase cancelOrderUseCase, RegisterClientUseCase registerClientUseCase)
        {
            bool exitClientMenu = false;

            while (!exitClientMenu)
            {
                Console.Clear();
                Console.WriteLine("Меню клиента:");
                Console.WriteLine("1. Зарегистрироваться");
                Console.WriteLine("2. Войти");
                Console.WriteLine("3. Сделать заказ");
                Console.WriteLine("4. Отменить заказ");
                Console.WriteLine("5. Назад");
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
                        CancelOrder(cancelOrderUseCase);
                        break;
                    case "5":
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
            Console.WriteLine($"Клиент {client.Name} успешно зарегистрирован с ID {client.ClientId}");
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
            createOrderUseCase.Execute(orderClientId, new List<(string color, string carBrand)>()
            {
                (carpetColor, carBrand)
            });

            Console.WriteLine($"Заказ создан с маркой автомобиля {carBrand} и цветом ковров {carpetColor}.");
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
        static void ShowManagerMenu(ManagerRepository managerRepository, CreateOrderUseCase createOrderUseCase, AssignMakerToOrderUseCase assignMakerToOrderUseCase, CompleteMakingUseCase completeMakingUseCase, SendToStoreUseCase sendToStoreUseCase, SendToDeliveryUseCase sendToDeliveryUseCase, RegisterManagerUseCase registerManagerUseCase, OrderRepository orderRepository)
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
                Console.WriteLine("8. Назад");
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
                        CompleteOrder(completeMakingUseCase);
                        break;
                    case "6":
                        SendToStore(sendToStoreUseCase);
                        break;
                    case "7":
                        SendToDelivery(sendToDeliveryUseCase);
                        break;
                    case "8":
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
            Console.Write("Введите имя менеджера для регистрации: ");
            string managerName = Console.ReadLine();
            var manager = registerManagerUseCase.Execute(managerName);
            Console.WriteLine($"Менеджер {manager.Name} успешно зарегистрирован с ID {manager.ManagerId}");
            Console.ReadKey();
        }

        static void LoginManager(ManagerRepository managerRepository)
        {
            Console.Write("Введите имя менеджера для входа: ");
            string managerName = Console.ReadLine();
            var manager = managerRepository.GetByName(managerName);
            if (manager != null)
            {
                Console.WriteLine($"Менеджер {manager.Name} вошел в систему.");
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
                Console.WriteLine($"Заказ  Статус: {order.Status}");
            }
            Console.ReadKey();
        }

        static void AssignOrderToMaker(AssignMakerToOrderUseCase assignMakerToOrderUseCase)
        {
            Console.Write("Введите ID заказа для назначения на изготовителя: ");
            int orderId = int.Parse(Console.ReadLine());
            Console.Write("Введите ID изготовителя: ");
            int makerId = int.Parse(Console.ReadLine());
            assignMakerToOrderUseCase.Execute(orderId, makerId);
            Console.WriteLine($"Заказ с ID {orderId} назначен на изготовителя с ID {makerId}");
            Console.ReadKey();
        }

        static void CompleteOrder(CompleteMakingUseCase completeMakingUseCase)
        {
            Console.Write("Введите ID заказа для изменения статуса на выполнено: ");
            int orderId = int.Parse(Console.ReadLine());
            completeMakingUseCase.Execute(orderId);
            Console.WriteLine($"Заказ с ID {orderId} был завершен.");
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
            Console.WriteLine("Введите адресс для доставки:");
            var address = Console.ReadLine();
            sendToDeliveryUseCase.Execute(orderId, address);
            Console.WriteLine($"Заказ с ID {orderId} отправлен на доставку по адрессу {address}");
            Console.ReadKey();
        }
    }
}
