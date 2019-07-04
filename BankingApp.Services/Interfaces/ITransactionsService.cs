using System;
using System.Collections.Generic;
using BankingApp.DataTransfer;

namespace BankingApp.Services.Interfaces
{
    public interface ITransactionsService
    {
        IEnumerable<TransactionDto> GetSentTransactions(Guid userId);
        IEnumerable<TransactionDto> GetReceivedTransactions(Guid userId);
    }
}