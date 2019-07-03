using System;

namespace BankingApp.DataTransfer
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public double Balance { get; set; }
    }
}