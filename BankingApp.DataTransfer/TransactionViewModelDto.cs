using System;
using BankingApp.Data.Entities;
using BankingApp.DataTransfer.Helpers;

namespace BankingApp.DataTransfer
{
    public class TransactionViewModelDto
    {
        public DateTimeOffset TimeOfTransaction { get; set; }

        public double Amount { get; set; }
        public string TransactionType { get; set; }
        public string SenderUserEmail { get; set; }
        public string ReceiverUserEmail { get; set; }

        public static TransactionViewModelDto ConvertFromTransaction(Transaction transaction) =>
            new TransactionViewModelDto()
            {
                TransactionType = transaction.TransactionType.ToString(),
                Amount = Casting.LongToDouble(transaction.Amount),
                TimeOfTransaction = transaction.TimeOfTransaction,
                SenderUserEmail = transaction.SenderUser?.Email,
                ReceiverUserEmail = transaction.ReceiverUser?.Email
            };
    }
}