using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long Balance { get; set; }

        [InverseProperty("SenderUser")]
        public virtual ICollection<Transaction> SentTransactions { get; set; } = new List<Transaction>();

        [InverseProperty("ReceiverUser")]
        public virtual ICollection<Transaction> ReceivedTransactions { get; set; } = new List<Transaction>();
    }
}
