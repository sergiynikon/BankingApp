using System;
using System.Collections.Generic;
using BankingApp.DataTransfer;

namespace BankingApp.Services.Interfaces
{
    public interface ITransactionsService
    {
        ResultDto GetUserTransactions(Guid userId);
    }
}