using System;
using BankingApp.DataTransfer;

namespace BankingApp.Services.Interfaces
{
    public interface IBankingLogicService
    {
        OperationDetailsDto Deposit(Guid senderUserId, double amount);
        OperationDetailsDto Withdraw(Guid senderUserId, double amount);
        OperationDetailsDto Transfer(Guid senderUserId, Guid receiverUserId, double amount);
    }
}