using System;
using BankingApp.DataTransfer;

namespace BankingApp.Services.Interfaces
{
    public interface IBankingLogicService
    {
        ResultDto Deposit(Guid senderUserId, OperationModelDto model);
        ResultDto Withdraw(Guid senderUserId, OperationModelDto model);
        ResultDto Transfer(Guid senderUserId, OperationModelDto model);
    }
}