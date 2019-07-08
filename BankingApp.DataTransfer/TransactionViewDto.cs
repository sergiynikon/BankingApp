using System;
using BankingApp.Data.Entities;

namespace BankingApp.DataTransfer
{
    public class TransactionViewDto
    {
        public DateTimeOffset TimeOfTransaction { get; set; }
        public double Amount { get; set; }
        public string TransactionType { get; set; }
        public string SenderUserEmail { get; set; }
        public string ReceiverUserEmail { get; set; }

        public static TransactionViewDto ConvertFromTransaction(Transaction transaction) =>
            new TransactionViewDto()
            {
                TransactionType = transaction.TransactionType.ToString(),
                Amount = transaction.Amount,
                TimeOfTransaction = transaction.TimeOfTransaction,
                SenderUserEmail = transaction.SenderUser?.Email,
                ReceiverUserEmail = transaction.ReceiverUser?.Email
            };
    }
}