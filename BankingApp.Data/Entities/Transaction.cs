using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingApp.Data.Entities
{
    public enum TransactionType
    {
        Deposit,
        Withdraw,
        Transfer
    }

    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTimeOffset TimeOfTransaction { get; set; }
        public long Amount { get; set; }
        public TransactionType TransactionType { get; set; }

        public Guid? ReceiverUserId { get; set; }
        public Guid? SenderUserId { get; set; }

        public virtual User ReceiverUser { get; set; }
        public virtual User SenderUser { get; set; }

        //TODO: make some constructors
    }
}
