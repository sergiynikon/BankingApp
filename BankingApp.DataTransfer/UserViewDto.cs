using BankingApp.Data.Entities;
using BankingApp.DataTransfer.Helpers;

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
                Balance = Casting.LongToDouble(user.Balance)
            };
    }
}