using System;
using System.Collections.Generic;
using System.Linq;
using BankingApp.Data.UnitOfWork.Interfaces;
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

        public UserViewDto GetUser(Guid id)
        {
            return UserViewDto.ConvertFromUser(_unitOfWork.UserRepository.GetById(id));
        }

        public IEnumerable<UserViewDto> GetAllUsers()
        {
            var users = new List<UserViewDto>();
            var usersFromDatabase = _unitOfWork.UserRepository.GetAll();
            foreach (var user in usersFromDatabase)
            {
                users.Add(UserViewDto.ConvertFromUser(user));
            }
            return users;
        }
    }
}