using System.ComponentModel.DataAnnotations;
using BankingApp.Data.Entities;

namespace BankingApp.DataTransfer
{
    public class RegisterDto
    {
        private const int MinPasswordLength = 5;

        [Required]
        public string Login { get; set; }

        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }

        [Required]
        [MinLength(MinPasswordLength)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public User ConvertToUser() => new User(Login, Email, Password);

    }
}