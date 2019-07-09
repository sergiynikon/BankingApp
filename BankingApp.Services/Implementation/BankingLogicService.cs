using System;
using System.Linq;
using BankingApp.Data.Entities;
using BankingApp.Data.UnitOfWork;
using BankingApp.DataTransfer;
using BankingApp.DataTransfer.Helpers;
using BankingApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Services.Implementation
{
    public enum OperationType
    {
        Deposit,
        Withdraw,
        Transfer
    }

    public class BankingLogicService : IBankingLogicService
    {
        private static int MaxAttempts = 10;
        private static readonly string ErrorMessageAmountNotGreaterThanZero = "operation can not be performed. Amount must be greater, than 0!";
        private static readonly string ErrorMessageCanNotFindReceiverUser = "Can not find receiver user!";
        private static readonly string ErrorMessageReceiverUserIdShouldBeDifferentFromYour = "Receiver user id should be different from your!";
        private static readonly string ErrorMessageNotEnoughMoney = "Not enough money!";

        private readonly IUnitOfWork _unitOfWork;

        public BankingLogicService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private ResultDto ExecuteOperation(Guid senderUserId, OperationModelDto model, OperationType operationType)
        {
            if (model.Amount <= 0)
            {
                return ResultDto.Error(ErrorMessageAmountNotGreaterThanZero,
                    new OperationDetailsDto(
                        amount: 0,
                        senderUserId: senderUserId,
                        receiverUserId: model.ReceiverUserId));
            }

            long amountInCents = Casting.DoubleToLong(model.Amount);
            double amount = Casting.LongToDouble(amountInCents);

            var senderUser = _unitOfWork.UserRepository.GetById(senderUserId);

            switch (operationType)
            {
                case OperationType.Deposit:
                    senderUser.Balance += amountInCents;

                    _unitOfWork.TransactionRepository.Add(
                        Transaction.CreateDepositTransaction(senderUserId, amountInCents));

                    break;

                case OperationType.Withdraw:
                    if (senderUser.Balance < amountInCents)
                    {
                        return ResultDto.Error(ErrorMessageNotEnoughMoney,
                            new OperationDetailsDto(
                                amount: 0,
                                senderUserId: senderUserId,
                                receiverUserId: model.ReceiverUserId));
                    }

                    senderUser.Balance -= amountInCents;

                    _unitOfWork.TransactionRepository.Add(
                        Transaction.CreateWithdrawTransaction(senderUserId, amountInCents));

                    break;

                case OperationType.Transfer:
                    var receiverUserId = model.ReceiverUserId ?? Guid.Empty;

                    var operationDetails = new OperationDetailsDto(
                        amount: 0,
                        senderUserId: senderUserId,
                        receiverUserId: model.ReceiverUserId);

                    var receiverUser = _unitOfWork.UserRepository.GetById(receiverUserId);

                    if (receiverUser == null)
                    {
                        return ResultDto.Error(ErrorMessageCanNotFindReceiverUser,
                            operationDetails);
                    }

                    if (model.ReceiverUserId == senderUserId)
                    {
                        return ResultDto.Error(ErrorMessageReceiverUserIdShouldBeDifferentFromYour,
                            operationDetails);
                    }

                    if (senderUser.Balance < amountInCents)
                    {
                        return ResultDto.Error(ErrorMessageNotEnoughMoney,
                            new OperationDetailsDto(
                                amount: 0,
                                senderUserId: senderUserId,
                                receiverUserId: model.ReceiverUserId));
                    }

                    senderUser.Balance -= amountInCents;
                    receiverUser.Balance += amountInCents;

                    _unitOfWork.TransactionRepository.Add(
                        Transaction.CreateTransferTransaction(senderUserId, receiverUserId, amountInCents));

                    break;
            }

            for (int i = 0; i < MaxAttempts; i++)
            { 
                try
                {
                    _unitOfWork.Save();

                    return ResultDto.Success(
                        new OperationDetailsDto(
                            amount: amount,
                            senderUserId: senderUserId,
                            receiverUserId: model.ReceiverUserId));
                }
                catch (DbUpdateConcurrencyException e)
                {
                    e.Entries.Single().Reload();
                }
                catch (Exception e)
                {
                    return ResultDto.Error(e.Message, e);
                }
            }

            return ResultDto.Error();
        }

        public ResultDto Deposit(Guid senderUserId, OperationModelDto model)
        {
            return ExecuteOperation(senderUserId, model, OperationType.Deposit);
        }

        public ResultDto Withdraw(Guid senderUserId, OperationModelDto model)
        {
            return ExecuteOperation(senderUserId, model, OperationType.Withdraw);
        }

        public ResultDto Transfer(Guid senderUserId, OperationModelDto model)
        {
            return ExecuteOperation(senderUserId, model, OperationType.Transfer);
        }
    }
}
