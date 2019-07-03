using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankingApp.Data.Helpers;


namespace BankingApp.Data.Entities
{
    public class User
    {
        public User(string login, string email, string password)
        {
            Id = new Guid(); //TODO: check whether it works without creating new Guid()
            Login = login;
            Email = email;
            Password = password;
            Balance = 0;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Login should be less than 20 symbols!")]
        public string Login { get; set; }

        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }

        [Required]
        [Range(Constants.MinPasswordLength, Constants.MaxPasswordLength)]
        public string Password { get; set; }

        public double Balance { get; set; }

        [InverseProperty("SenderUser")]
        public virtual ICollection<Transaction> SentTransactions { get; set; } = new List<Transaction>();

        [InverseProperty("ReceiverUser")]
        public virtual ICollection<Transaction> ReceivedTransactions { get; set; } = new List<Transaction>();
    }
}