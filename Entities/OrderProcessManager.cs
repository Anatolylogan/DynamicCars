using Domain.Entities;
using Domain.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class OrderProcessManager
    {
        private readonly OrderService orderService;
        private readonly ManagerService managerService;
        private readonly MakerService makerService;
        private readonly StoreService storeService;
        private readonly DeliveryService deliveryService;
        public OrderProcessManager(
            OrderService orderSvc,
            ManagerService mangerSvc,
            MakerService makerSvc,
            StoreService storeSvc,
            DeliveryService deliverySvc)
        {
            orderService = orderSvc;
            managerService = mangerSvc;
            makerService = makerSvc;
            storeService = storeSvc;
            deliveryService = deliverySvc;
        }
        public void ProcessOrder(Client client, List<(string color, string carBrand)> matChoices, Maker maker, string address)// Метод который выполняет весь процесс
        {
            Order order = orderService.CreateOrder(client, matChoices);
            managerService.AssignMaker(order, maker);
            makerService.CompleteMaking(order);
            storeService.SendTOStore(order);
            deliveryService.SendToDelivery(order, address);
            deliveryService.CompleteDelivery(order);
        }
    }
}
