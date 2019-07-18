using System;
using BankingApp.Data.Entities;
using BankingApp.DataTransfer.Helpers;

namespace BankingApp.DataTransfer
{
    public class UserViewModelDto
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Balance { get; set; }

        public static UserViewModelDto ConvertFromUser(User user) =>
            new UserViewModelDto()
            {
                Id = user.Id,
                Login = user.Login,
                Email = user.Email,
                Balance = Casting.LongToDouble(user.Balance).ToString("0.00")
            };
    }
}