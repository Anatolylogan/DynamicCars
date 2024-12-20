﻿using Domain.Entities;
using Infrastructure.Repository;

namespace Domain.UseCase
{
    public class RegisterManagerUseCase
    {
        private readonly ManagerRepository _managerRepository;

        public RegisterManagerUseCase(ManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        public Manager Execute(string managerName)
        {
            if (string.IsNullOrWhiteSpace(managerName))
            {
                throw new ArgumentException("Имя менеджера не может быть пустым.");
            }

            if (_managerRepository.GetByName(managerName) != null)
            {
                throw new InvalidOperationException("Менеджер с таким именем уже существует.");
            }

            var manager = new Manager { Name = managerName };
            _managerRepository.Add(manager);

            return manager;
        }
    }
}