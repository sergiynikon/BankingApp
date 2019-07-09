using System;
using System.Linq;
using BankingApp.Data.Entities;
using BankingApp.Data.UnitOfWork;
using BankingApp.DataTransfer;
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
        private static readonly int CentValue = 100;
        private static readonly string ErrorMessageAmountNotGreaterThanZero = "operation can not be performed. Amount must be greater, than 0!";
        private static readonly string ErrorMessageReceiverUserShouldNotBeSet = "ReceiverUserId should not be set!";
        private static readonly string ErrorMessageCanNotFindReceiverUser = "Can not find receiver user!";
        private static readonly string ErrorMessageReceiverUserIdShouldBeDifferentFromYour = "Receiver user id should be different from your!";
        private static readonly string ErrorMessageNotEnoughMoney = "Not enough money!";

        private readonly IUnitOfWork _unitOfWork;

        public BankingLogicService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private long CastFromDouble(double value)
        {
            return (long) (value * CentValue);
        }

        private double CastFromLong(long value)
        {
            return value / (double)CentValue;
        }

        private ResultDto ExecuteOperation(Guid senderUserId, OperationModelDto model, OperationType operationType)
        {
            long longAmount = CastFromDouble(model.Amount);

            if (longAmount <= 0)
            {
                return ResultDto.Error(ErrorMessageAmountNotGreaterThanZero,
                    new OperationDetailsDto(
                        amount: 0,
                        senderUserId: senderUserId));
            }

            var senderUser = _unitOfWork.UserRepository.GetById(senderUserId);
            var receiverUser = _unitOfWork.UserRepository.GetById(model.ReceiverUserId ?? Guid.Empty);

            //ReceiverUserId must be null when deposit or withdraw
            if ((operationType == OperationType.Deposit 
                 || operationType == OperationType.Withdraw) 
                && model.ReceiverUserId != null)
            {
                return ResultDto.Error(ErrorMessageReceiverUserShouldNotBeSet);
            }

            if (operationType == OperationType.Transfer)
            {
                var operationDetails = new OperationDetailsDto(
                    amount: 0,
                    senderUserId: senderUserId,
                    receiverUserId: model.ReceiverUserId);

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
            }

            if ((operationType == OperationType.Withdraw 
                 || operationType == OperationType.Transfer) 
                && senderUser.Balance < longAmount)
            {
                return ResultDto.Error(ErrorMessageNotEnoughMoney,
                    new OperationDetailsDto(
                        amount: 0,
                        senderUserId: senderUserId,
                        receiverUserId: model.ReceiverUserId));
            }
            switch (operationType)
            {
                case OperationType.Deposit:
                    senderUser.Balance += longAmount;

                    _unitOfWork.TransactionRepository.Add(new Transaction(
                        senderUserId: senderUserId,
                        receiverUserId: null,
                        amount: longAmount,
                        transactionType: TransactionType.Deposit));

                    break;

                case OperationType.Withdraw:
                    senderUser.Balance -= longAmount;

                    _unitOfWork.TransactionRepository.Add(new Transaction(
                        senderUserId: senderUserId,
                        receiverUserId: null,
                        amount: longAmount,
                        transactionType: TransactionType.Withdraw));

                    break;

                case OperationType.Transfer:
                    senderUser.Balance -= longAmount;
                    receiverUser.Balance += longAmount;

                    _unitOfWork.TransactionRepository.Add(new Transaction(
                        senderUserId: senderUserId,
                        receiverUserId: model.ReceiverUserId,
                        amount: longAmount,
                        transactionType: TransactionType.Transfer));

                    break;
            }

            try
            {
                _unitOfWork.Save();

                var resultAmount = CastFromLong(longAmount);

                return ResultDto.Success(
                    new OperationDetailsDto(
                        amount: resultAmount,
                        senderUserId: senderUserId,
                        receiverUserId: model.ReceiverUserId));
            }
            catch (DbUpdateConcurrencyException e)
            {
                e.Entries.Single().Reload();
                return ExecuteOperation(senderUserId, model, operationType);
            }
            catch (Exception e)
            {
                return ResultDto.Error(e.Message, e);
            }
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
