using System;
using System.Collections.Generic;
using BankingApp.Data.Entities;
using BankingApp.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Data.Repositories.Implementation
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Transaction> GetSentTransactionsByUserId(Guid id)
        {
            return base.Find(t => t.SenderUserId == id);
        }

        public IEnumerable<Transaction> GetReceivedTransactionsByUserId(Guid id)
        {
            return base.Find(t => t.ReceiverUserId == id);
        }
    }
}