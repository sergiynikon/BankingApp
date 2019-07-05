using System;
using BankingApp.DataTransfer;

namespace BankingApp.Services.Interfaces
{
    public interface IBankingLogicService
    {
        ResultDto Deposit(Guid senderUserId, double amount);
        ResultDto Withdraw(Guid senderUserId, double amount);
        ResultDto Transfer(Guid senderUserId, Guid receiverUserId, double amount);
    }
}