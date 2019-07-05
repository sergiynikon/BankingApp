using System;
using BankingApp.DataTransfer;

namespace BankingApp.Services.Interfaces
{
    public interface IBankingLogicService
    {
        OperationDetailsDto Deposit(double amount, Guid senderUserId);
        OperationDetailsDto Withdraw(double amount);
        OperationDetailsDto Transfer(double amount, Guid receiverUserId);

    }
}