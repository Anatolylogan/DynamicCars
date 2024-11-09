﻿using Domain.Entities;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase
{
    public class ManagerService
    {
        private readonly OrderRepository orderRepository;
        private readonly MakerRepository makerRepository;
        public ManagerService(OrderRepository orderRepo, MakerRepository makerRepo)
        {
            orderRepository = orderRepo;
            makerRepository = makerRepo;
        }

        public void AssignMaker(Order order, Maker maker) //Метод для назанчение Maker на заказ
        {
            order.Maker = maker;
            order.MakerId = maker.MakerId;
            order.Status = "Назначаен изготовителю";
        }
    }
}