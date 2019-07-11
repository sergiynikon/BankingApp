using BankingApp.Data.Entities;
using BankingApp.DataTransfer.Helpers;

namespace BankingApp.DataTransfer
{
    public class UserViewModelDto
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public double Balance { get; set; }

        public static UserViewModelDto ConvertFromUser(User user) =>
            new UserViewModelDto()
            {
                Login = user.Login,
                Email = user.Email,
                Balance = Casting.LongToDouble(user.Balance)
            };
    }
}