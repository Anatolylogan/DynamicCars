
using Domain.Entities;
using Domain.Repository;
using Domain.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

public class ConsoleUi
{
    private readonly ClientRepository _clientRepository;
    private readonly OrderRepository _orderRepository;
    private readonly MakerRepository _makerRepository;
    private readonly StoreRepository _storeRepository;
    private readonly DeliveryRepository _deliveryRepository;

    private readonly RegisterClientUseCase _registerClientUseCase;
    private readonly CreateOrderUseCase _createOrderUseCase;
    private readonly StoreService storeService;

    public ConsoleUi()
    {
        _clientRepository = new ClientRepository();
        _orderRepository = new OrderRepository();
        _makerRepository = new MakerRepository();
        _storeRepository = new StoreRepository();
        _deliveryRepository = new DeliveryRepository();
        _registerClientUseCase = new RegisterClientUseCase(_clientRepository);
        _createOrderUseCase = new CreateOrderUseCase(_orderRepository, _clientRepository);
        _storeRepository = new StoreRepository();
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("Главное меню:");
            Console.WriteLine("1. Клиент");
            Console.WriteLine("2. Менеджер");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите пункт: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowClientMenu();
                    break;
                case "2":
                    ShowManagerMenu();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }
    private void ShowClientMenu()
    {
        while (true)
        {
            Console.WriteLine("Меню клиента:");
            Console.WriteLine("1. Зарегистрироваться");
            Console.WriteLine("2. Войти");
            Console.WriteLine("3. Сделать заказ");
            Console.WriteLine("4. Отменить заказ");
            Console.WriteLine("0. Назад");
            Console.Write("Выберите пункт: ");
            string choice = Console.ReadLine();

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
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }
    private void ShowManagerMenu()
    {
        while (true)
        {
            Console.WriteLine("Меню менеджера:");
            Console.WriteLine("1. Список заказов");
            Console.WriteLine("2. Назначить заказ на исполнителя");
            Console.WriteLine("3. Изменить статус заказа на выполнено");
            Console.WriteLine("4. Отправить заказ на склад");
            Console.WriteLine("5. Отправить заказ на доставку");
            Console.WriteLine("0. Назад");
            Console.Write("Выберите пункт: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowOrders();
                    break;
                case "2":
                    AssignMaker();
                    break;
                case "3":
                    MarkOrderAsCompleted();
                    break;
                case "4":
                    SendOrderToStore();
                    break;
                case "5":
                    SendOrderToDelivery();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }

    private void RegisterClient()
    {
        Console.Write("Введите имя клиента: ");
        string name = Console.ReadLine();
        var client = _registerClientUseCase.Execute(name);
        Console.WriteLine($"Клиент зарегистрирован. ID: {client.ClientId}, Имя: {client.Name}");
    }

    private void LoginClient()
    {
        Console.Write("Введите ID клиента: ");
        if (int.TryParse(Console.ReadLine(), out int clientId))
        {
            var client = _clientRepository.GetById(clientId);
            if (client != null)
            {
                Console.WriteLine($"Добро пожаловать, {client.Name}!");
            }
            else
            {
                Console.WriteLine("Клиент с таким ID не найден.");
            }
        }
        else
        {
            Console.WriteLine("ID должен быть числом.");
        }
    }
    private void MakeOrder()
    {
        Console.Write("Введите ID клиента: ");
        if (!int.TryParse(Console.ReadLine(), out int clientId))
        {
            Console.WriteLine("ID должен быть числом.");
            return;
        }

        var client = _clientRepository.GetById(clientId);
        if (client == null)
        {
            Console.WriteLine("Клиент с таким ID не найден.");
            return;
        }

        Console.WriteLine("Введите информацию о ковриках (цвет и марка автомобиля).");
        var mats = new List<(string color, string carBrand)>();
        while (true)
        {
            Console.Write("Цвет коврика (или пусто для завершения): ");
            string color = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(color)) break;

            Console.Write("Марка автомобиля: ");
            string carBrand = Console.ReadLine();

            mats.Add((color, carBrand));
        }

        if (mats.Any())
        {
            var order = _createOrderUseCase.Execute(clientId, mats);
            Console.WriteLine($"Заказ создан. ID: {order.OrderID}, Статус: {order.Status}");
        }
        else
        {
            Console.WriteLine("Заказ не создан. Не указаны коврики.");
        }
    }
    private void CancelOrder()
    {
        Console.Write("Введите ID заказа для отмены: ");
        if (int.TryParse(Console.ReadLine(), out int orderId))
        {
            var order = _orderRepository.GetById(orderId);
            if (order != null)
            {
                _orderRepository.GetAll().Remove(order);
                Console.WriteLine($"Заказ с ID {orderId} отменён.");
            }
            else
            {
                Console.WriteLine("Заказ с таким ID не найден.");
            }
        }
        else
        {
            Console.WriteLine("ID должен быть числом.");
        }
    }
    private void ShowOrders()
    {
        var orders = _orderRepository.GetAll();
        if (orders.Any())
        {
            Console.WriteLine("Список заказов:");
            foreach (var order in orders)
            {
                Console.WriteLine($"ID: {order.OrderID}, Статус: {order.Status}");
            }
        }
        else
        {
            Console.WriteLine("Нет заказов.");
        }
    }
    private void AssignMaker()
    {
        Console.Write("Введите ID заказа: ");
        if (!int.TryParse(Console.ReadLine(), out int orderId))
        {
            Console.WriteLine("ID заказа должен быть числом.");
            return;
        }

        var order = _orderRepository.GetById(orderId);
        if (order == null)
        {
            Console.WriteLine("Заказ с таким ID не найден.");
            return;
        }

        Console.Write("Введите ID изготовителя: ");
        if (!int.TryParse(Console.ReadLine(), out int makerId))
        {
            Console.WriteLine("ID изготовителя должен быть числом.");
            return;
        }

        var maker = _makerRepository.GetById(makerId);
        if (maker == null)
        {
            Console.WriteLine("Изготовитель с таким ID не найден.");
            return;
        }

        order.Maker = maker;
        order.Status = "Assigned to maker";
        _orderRepository.Update(order);

        Console.WriteLine($"Изготовитель с ID {makerId} назначен на заказ {orderId}. Статус заказа обновлён: {order.Status}");
    }
    private void MarkOrderAsCompleted()
    {
        Console.Write("Введите ID заказа: ");
        if (!int.TryParse(Console.ReadLine(), out int orderId))
        {
            Console.WriteLine("ID заказа должен быть числом.");
            return;
        }

        var order = _orderRepository.GetById(orderId);
        if (order == null)
        {
            Console.WriteLine("Заказ с таким ID не найден.");
            return;
        }

        if (order.Status != "Assigned to maker")
        {
            Console.WriteLine("Заказ не может быть выполнен, так как изготовитель не назначен.");
            return;
        }

        order.Status = "Made";
        _orderRepository.Update(order);

        Console.WriteLine($"Заказ с ID {orderId} выполнен. Статус обновлён: {order.Status}");
    }
    private void SendOrderToDelivery()
    {
        Console.Write("Введите ID заказа: ");
        if (!int.TryParse(Console.ReadLine(), out int orderId))
        {
            Console.WriteLine("ID заказа должен быть числом.");
            return;
        }

        var order = _orderRepository.GetById(orderId);
        if (order == null)
        {
            Console.WriteLine("Заказ с таким ID не найден.");
            return;
        }

        if (order.Status != "Made")
        {
            Console.WriteLine("Заказ должен быть выполнен перед отправкой на доставку.");
            return;
        }

        Console.Write("Введите адрес доставки: ");
        string address = Console.ReadLine();

        var delivery = new Delivery
        {
            Order = order,
            Address = address
        };

        order.Status = "Sent for delivery";
        _deliveryRepository.Add(delivery);
        _orderRepository.Update(order);

        Console.WriteLine($"Заказ с ID {orderId} отправлен на доставку по адресу: {address}. Статус обновлён: {order.Status}");
    }
    public void SendOrderToStore()
    {
        Console.WriteLine("\n=== Отправка заказа на склад ===");


        var orders = _orderRepository.GetAll();
        if (!orders.Any())
        {
            Console.WriteLine("Нет доступных заказов.");
            return;
        }

        Console.WriteLine("Доступные заказы:");
        foreach (var order in orders)
        {
            Console.WriteLine($"ID: {order.OrderID}, Клиент: {order.Client.Name}, Статус: {order.Status}");
        }
        Console.Write("Введите ID заказа для отправки на склад: ");
        if (int.TryParse(Console.ReadLine(), out int orderId))
        {
            var order = _orderRepository.GetById(orderId);

            if (order == null)
            {
                Console.WriteLine("Заказ с таким ID не найден.");
                return;
            }

            if (order.Status != "Made")
            {
                Console.WriteLine("Только заказы со статусом 'Made' могут быть отправлены на склад.");
                return;
            }
            _storeService.SendToStore(order);
            Console.WriteLine($"Заказ ID: {order.OrderID} успешно отправлен на склад.");
        }
        else
        {
            Console.WriteLine("Неверный ввод ID заказа.");
        }
    }
}

