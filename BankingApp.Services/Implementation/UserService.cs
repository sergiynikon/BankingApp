using System;
using System.Collections.Generic;
using System.Linq;
using BankingApp.Data.UnitOfWork.Interfaces;
using BankingApp.DataTransfer;
using BankingApp.Services.Interfaces;

namespace BankingApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public UserDto GetUser(Guid id)
        {
            return UserDto.ConvertFromUser(_unitOfWork.UserRepository.GetById(id));
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = new List<UserDto>();
            var usersFromDatabase = _unitOfWork.UserRepository.GetAll();
            foreach (var user in usersFromDatabase)
            {
                users.Add(UserDto.ConvertFromUser(user));
            }
            return users;
        }
    }
}