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
    }
}