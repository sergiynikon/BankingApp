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

        public virtual ICollection<Transaction> SentTransactions { get; set; } = new List<Transaction>();
        public virtual ICollection<Transaction> ReceivedTransactions { get; set; } = new List<Transaction>();

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
