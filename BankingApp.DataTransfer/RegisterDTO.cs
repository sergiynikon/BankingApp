using System.ComponentModel.DataAnnotations;
using BankingApp.Data.Entities;
using BankingApp.Data.Helpers;

namespace BankingApp.DataTransfer
{
    public class RegisterDto
    {
        private const int MinPasswordLength = 5;

        [Required]
        public string Login { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(MinPasswordLength)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        public User ConvertToUser()
        {
            return new User(
                login: Login,
                email: Email,
                password: Encrypt.GetHash(Password));
        }
    }
}