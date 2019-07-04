using System;
using System.Collections.Generic;
using BankingApp.DataTransfer;

namespace BankingApp.Services.Interfaces
{
    public interface ITransactionsService
    {
        IEnumerable<TransactionViewDto> GetSentTransactions(Guid userId);
        IEnumerable<TransactionViewDto> GetReceivedTransactions(Guid userId);
    }
}