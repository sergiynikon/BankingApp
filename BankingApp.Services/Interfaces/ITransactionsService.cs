using System;
using System.Collections.Generic;
using BankingApp.Data.Entities;

namespace BankingApp.Services.Interfaces
{
    public interface ITransactionsService
    {
        IEnumerable<Transaction> GetSentTransactions(Guid userId);
        IEnumerable<Transaction> GetReceivedTransactions(Guid userId);
    }
}