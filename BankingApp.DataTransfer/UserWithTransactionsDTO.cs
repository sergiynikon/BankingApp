using System.Collections.Generic;
using BankingApp.Data.Entities;

namespace BankingApp.DataTransfer
{
    public class UserWithTransactionsDto
    {
        public string Login { get; set; }
        public IEnumerable<Transaction> SentTransactions { get; set; }
        public IEnumerable<Transaction> ReceivedTransactions { get; set; }
    }
}