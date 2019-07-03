using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankingApp.Data.Enums;

namespace BankingApp.Data.Entities
{
    public class Transaction
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public DateTimeOffset TimeOfTransaction { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public TransactionType TransactionType { get; set; }

        public Guid? ReceiverUserId { get; set; } //TODO: check whether it works without nullable

        public Guid? SenderUserId { get; set; } //TODO: check whether it works without nullable

        [ForeignKey("ReceiverUserId")]
        public virtual User ReceiverUser { get; set; }

        [ForeignKey("SenderUserId")]
        public virtual User SenderUser { get; set; }

        //TODO: make some constructors
    }
}