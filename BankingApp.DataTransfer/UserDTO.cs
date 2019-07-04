using System;
using System.ComponentModel.DataAnnotations;
using BankingApp.Data.Entities;

namespace BankingApp.DataTransfer
{
    public class UserDto
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public double Balance { get; set; }

        public static UserDto ConvertFromUser(User user) =>
            new UserDto()
            {
                Login = user.Login,
                Email = user.Email,
                Balance = user.Balance
            };
    }
}