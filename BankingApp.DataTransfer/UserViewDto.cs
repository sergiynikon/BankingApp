using System;
using System.ComponentModel.DataAnnotations;
using BankingApp.Data.Entities;

namespace BankingApp.DataTransfer
{
    public class UserViewDto
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public double Balance { get; set; }

        public static UserViewDto ConvertFromUser(User user) =>
            new UserViewDto()
            {
                Login = user.Login,
                Email = user.Email,
                Balance = user.Balance
            };
    }
}