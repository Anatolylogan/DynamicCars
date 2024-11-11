using Domain.Entities;
using Domain.Repository;
using Domain.UseCase;
using Domain;
using System;
using System.Collections.Generic;

class Program
{
    static ClientRepository clientRepository = new ClientRepository();
    static OrderRepository orderRepository = new OrderRepository();
    static MakerRepository makerRepository = new MakerRepository();
    static StoreRepository storeRepository = new StoreRepository();
    static DeliveryRepository deliveryRepository = new DeliveryRepository();
    static ManagerRepository managerRepository = new ManagerRepository();

    static OrderService orderService = new OrderService(orderRepository, clientRepository);
    static ManagerService managerService = new ManagerService(orderRepository, makerRepository, managerRepository);
    static MakerService makerService = new MakerService(orderRepository);
    static StoreService storeService = new StoreService(storeRepository);
    static DeliveryService deliveryService = new DeliveryService(deliveryRepository, orderRepository);
    static OrderProcessManager processManager = new OrderProcessManager(
        orderService, managerService, makerService, storeService, deliveryService);
    static Client currentClient;
    static Manager currentManager;
    static List<(string color, string carBrand)> matChoices = new List<(string, string)>(); 

    static void Main()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("=== Добро пожаловать в Dynamic Cars ===");
            Console.WriteLine("1. Меню клиента");
            Console.WriteLine("2. Меню менеджера");
            Console.WriteLine("3. Выход");
            Console.Write("Выберите пункт: ");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ShowClientMenu();
                    break;
                case "2":
                    ShowManagerMenu();
                    break;
                case "3":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Повторите попытку.");
                    break;
            }
        }
    }
    static void ShowClientMenu()
    {
        bool back = false;
        while (!back)
        {
            Console.Clear();
            Console.WriteLine("=== Меню клиента ===");
            Console.WriteLine("1. Зарегистрироваться");
            Console.WriteLine("2. Войти");
            Console.WriteLine("3. Сделать заказ");
            Console.WriteLine("4. Отменить заказ");
            Console.WriteLine("5. Назад");
            Console.Write("Выберите пункт: ");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    RegisterClient();
                    break;
                case "2":
                    LoginClient();
                    break;
                case "3":
                    MakeOrder();
                    break;
                case "4":
                    CancelOrder();
                    break;
                case "5":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Повторите попытку.");
                    break;
            }
        }
    }
    static void RegisterClient()
    {
        Console.Write("Введите имя клиента: ");
        string name = Console.ReadLine();
        currentClient = new Client { ClientId = new Random().Next(1, 1000), Name = name };
        clientRepository.Add(currentClient);
        Console.WriteLine("Клиент успешно зарегистрирован. Нажмите любую клавишу для возврата.");
        Console.ReadKey();
    }
    static void LoginClient()
    {
        Console.Write("Введите имя клиента: ");
        string name = Console.ReadLine();
        currentClient = clientRepository.GetByName(name);
        Console.WriteLine(currentClient != null ? "Вход выполнен. Нажмите любую клавишу для возврата. " : "Клиент не найден. Нажмите любую клавишу для возврата.");
        Console.ReadKey();
    }
    static void MakeOrder()
    {
        if (currentClient == null)
        {
            Console.WriteLine("Пожалуйста, войдите в систему. Нажмите любую клавишу для возврата.");
            Console.ReadKey();
            return;
        }
        Console.Write("Введите цвет EVA коврика: ");
        string color = Console.ReadLine();
        Console.Write("Введите марку автомобиля: ");
        string carBrand = Console.ReadLine();
        matChoices.Add((color, carBrand));
        Order order = orderService.CreateOrder(currentClient, matChoices);
        orderRepository.Add(order);
        Console.WriteLine("Заказ успешно создан. Нажмите любую клавишу для возврата.");
        Console.ReadKey();
    }
    static void CancelOrder()
    {
        Console.Write("Введите ID заказа для отмены: ");
        int id = int.Parse(Console.ReadLine());
        Order order = orderRepository.GetById(id);
        if (order != null && order.Client == currentClient)
        {
            order.Status = "Отменен";
            orderRepository.Update(order);
            Console.WriteLine("Заказ успешно отменен.");
        }
        else
        {
            Console.WriteLine("Заказ не найден или доступ запрещен.");
        }
        Console.ReadKey();
    }
    static void ShowManagerMenu()
    {
        bool back = false;
        while (!back)
        {
            Console.Clear();
            Console.WriteLine("=== Меню менеджера ===");
            Console.WriteLine("1. Зарегистрироваться как менеджер");
            Console.WriteLine("2. Войти как менеджер");
            Console.WriteLine("3. Список заказов");
            Console.WriteLine("4. Назначить заказ на изготовителя");
            Console.WriteLine("5. Изменить статус заказа на выполнено");
            Console.WriteLine("6. Отправить заказ на склад");
            Console.WriteLine("7. Отправить заказ на доставку");
            Console.WriteLine("8. Назад");
            Console.Write("Выберите пункт: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RegisterManager();
                    break;
                case "2":
                    LoginManager();
                    break;
                case "3":
                    ListOrders();
                    break;
                case "4":
                    AssignMakerToOrder();
                    break;
                case "5":
                    SetOrderAsCompleted();
                    break;
                case "6":
                    SendOrderToStore();
                    break;
                case "7":
                    SendOrderForDelivery();
                    break;
                case "8":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Повторите попытку.");
                    break;
            }
        }
    }
    static void RegisterManager()
    {
        Console.Write("Введите имя менеджера: ");
        string name = Console.ReadLine();
        currentManager = new Manager { ManagerId = new Random().Next(1, 1000), Name = name };
        managerRepository.Add(currentManager);
        Console.WriteLine("Менеджер успешно зарегистрирован. Нажмите любую клавишу для возврата.");
        Console.ReadKey();
    }
    static void LoginManager()
    {
        Console.Write("Введите имя менеджера: ");
        string name = Console.ReadLine();
        currentManager = managerRepository.GetByName(name);
        Console.WriteLine(currentManager != null ? "Вход выполнен." : "Менеджер не найден.");
        Console.ReadKey();
    }
    static void ListOrders()
    {
        Console.Clear();
        Console.WriteLine("=== Список заказов ===");

        var orders = orderRepository.GetAll();
        if (orders.Any())
        {
            foreach (var order in orders)
            {
                Console.WriteLine($"ID заказа: {order.OrderID}, Статус: {order.Status}, ID клиента: {order.ClientId}");
                Console.WriteLine("Выбор EVA ковриков:");

                foreach (var mat in order.MatChoices)
                {
                    Console.WriteLine($" - Цвет: {mat.color}, Марка автомобиля: {mat.carBrand}");
                }
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Заказы отсутствуют.");
        }

        Console.ReadKey();
    }
    static void AssignMakerToOrder()
    {
        Console.Write("Введите ID заказа для назначения изготовителя: ");
        int orderId = int.Parse(Console.ReadLine());
        Order order = orderRepository.GetById(orderId);

        Console.Write("Введите имя изготовителя: ");
        string makerName = Console.ReadLine();
        Maker maker = new Maker { MakerId = new Random().Next(1, 1000), Name = makerName };
        makerRepository.Add(maker);

        if (order != null)
        {
            managerService.AssignMaker(order, maker);
            Console.WriteLine("Изготовитель успешно назначен. Нажмите любую клавишу для возврата.");
        }
        else
        {
            Console.WriteLine("Заказ не найден. Нажмите любую клавишу для возврата.");
        }
        Console.ReadKey();
    }
    static void SetOrderAsCompleted()
    {
        Console.Write("Введите ID заказа для завершения: ");
        int orderId = int.Parse(Console.ReadLine());
        Order order = orderRepository.GetById(orderId);

        if (order != null)
        {
            makerService.CompleteMaking(order);
            Console.WriteLine("Заказ успешно выполнен. Нажмите любую клавишу для возврата.");
        }
        else
        {
            Console.WriteLine("Заказ не найден. Нажмите любую клавишу для возврата.");
        }
        Console.ReadKey();
    }
    static void SendOrderToStore()
    {
        Console.Write("Введите ID заказа для отправки на склад: ");
        if (int.TryParse(Console.ReadLine(), out int orderId))
        {
            Order order = orderRepository.GetById(orderId);

            if (order == null)
            {
                Console.WriteLine("Заказ не найден. Нажмите любую клавишу для возврата.");
            }
            else
            {
                storeService.SendTOStore(order); 
                Console.WriteLine("Заказ успешно отправлен на склад. Нажмите любую клавишу для возврата.");
            }
        }
        else
        {
            Console.WriteLine("Некорректный ввод. Введите числовой ID. Нажмите любую клавишу для возврата.");
        }
        Console.ReadKey();
    }

    static void SendOrderForDelivery()
    {
        Console.Write("Введите ID заказа для отправки на доставку: ");
        int orderId = int.Parse(Console.ReadLine());
        Order order = orderRepository.GetById(orderId);

        Console.Write("Введите адрес доставки: ");
        string deliveryAddress = Console.ReadLine();

        if (order != null)
        {
            deliveryService.SendToDelivery(order, deliveryAddress);
            Console.WriteLine("Заказ успешно отправлен на доставкую. Нажмите любую клавишу для возврата.");
        }
        else
        {
            Console.WriteLine("Заказ не найден. Нажмите любую клавишу для возврата.");
        }
        Console.ReadKey();
    }
}
