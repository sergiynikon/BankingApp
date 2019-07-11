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
        public Transaction(Guid? senderUserId, Guid? receiverUserId, long amount, TransactionType transactionType)
        {
            TimeOfTransaction = DateTimeOffset.Now;
            Amount = amount;
            SenderUserId = senderUserId;
            ReceiverUserId = receiverUserId;
            TransactionType = transactionType;
        }

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

        public static Transaction CreateDepositTransaction(Guid senderUserId, long amount)
        {
            return new Transaction(
                senderUserId: senderUserId,
                receiverUserId: null,
                amount: amount,
                transactionType: TransactionType.Deposit);
        }

        public static Transaction CreateWithdrawTransaction(Guid senderUserId, long amount)
        {
            return new Transaction(
                senderUserId: senderUserId,
                receiverUserId: null,
                amount: amount,
                transactionType: TransactionType.Withdraw);
        }

        public static Transaction CreateTransferTransaction(Guid senderUserId, Guid receiverUserId, long amount)
        {
            return new Transaction(
                senderUserId: senderUserId,
                receiverUserId: receiverUserId,
                amount: amount,
                transactionType: TransactionType.Transfer);
        }
    }
}
