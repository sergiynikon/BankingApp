using System;
using System.Collections.Generic;
using BankingApp.Data.Entities;

namespace BankingApp.Data.Repositories.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        IEnumerable<Transaction> GetSentTransactionsByUserId(Guid id);
        IEnumerable<Transaction> GetReceivedTransactionsByUserId(Guid id);
    }
}