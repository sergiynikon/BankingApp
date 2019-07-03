using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingApp.Data.Entities
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string TimeOfTransaction { get; set; } = DateTime.Now.ToLongDateString();

        [Required]
        public double Amount { get; set; }

        [ForeignKey("ReceiverUserId")]
        public virtual User ReceiverUser { get; set; }
        public Guid? ReceiverUserId { get; set; }

        [ForeignKey("SenderUserId")]
        public virtual User SenderUser { get; set; }
        public Guid? SenderUserId { get; set; }

        //TODO: add property of transaction type
        //TODO: make several constructors
    }
}