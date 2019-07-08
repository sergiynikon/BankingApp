using System.ComponentModel.DataAnnotations;

namespace BankingApp.DataTransfer
{
    public class LoginDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}