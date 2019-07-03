using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingApp.Data.Entities
{
    public class User
    {
        public User()
        {
        }

        public User(string login, string email, string password)
        {
            Id = new Guid();
            Login = login;
            Email = email;
            Password = password;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Login should be less than 20 symbols!")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public double Balance { get; set; } = 0;

        [InverseProperty("SenderUser")]
        public virtual ICollection<Transaction> SentTransactions { get; set; } = new List<Transaction>();

        [InverseProperty("ReceiverUser")]
        public virtual ICollection<Transaction> ReceivedTransactions { get; set; } = new List<Transaction>();

    }
}