using System;
using System.Collections.Generic;
using BankingApp.Data.UnitOfWork;
using BankingApp.DataTransfer;
using BankingApp.Services.Interfaces;

namespace BankingApp.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public UserViewModelDto GetUser(Guid id)
        {
            return UserViewModelDto.ConvertFromUser(_unitOfWork.UserRepository.GetById(id));
        }

        public IEnumerable<UserViewModelDto> GetAllUsers()
        {
            var users = new List<UserViewModelDto>();
            var usersFromDatabase = _unitOfWork.UserRepository.GetAll();

            foreach (var user in usersFromDatabase)
            {
                users.Add(UserViewModelDto.ConvertFromUser(user));
            }

            return users;
        }
    }
}