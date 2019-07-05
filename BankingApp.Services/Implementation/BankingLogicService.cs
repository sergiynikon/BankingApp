using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingApp.Data.Entities;
using BankingApp.Data.UnitOfWork;
using BankingApp.DataTransfer;
using BankingApp.Services.Interfaces;

namespace BankingApp.Services.Implementation
{
    public class BankingLogicService : IBankingLogicService
    {
        private static readonly int CentValue = 100;
        private static readonly string ErrorMessageDepositNonPositiveAmount = "Can not deposit non positive amount";
        private static readonly string ErrorMessageWithdrawNonPositiveAmount = "Can not withdraw non positive amount";
        private static readonly string ErrorMessageWithdrawNotEnoughMoney = "Can not withdraw. Not enough money";
        private static readonly string ErrorMessageTransferWhenReceiverUserNotFound = "Can not find receiver";
        private static readonly string ErrorMessageTransferNonPositiveAmount = "Can not transfer non positive amount";
        private static readonly string ErrorMessageTransferNotEnoughMoney = "Can not transfer. Not enough money";

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
            return (value / (double)CentValue);
        }

        public ResultDto Deposit(Guid senderUserId, double amount)
        {
            long longAmount = CastFromDouble(amount);

            if (longAmount <= 0)
            {
                return ResultDto.Error(ErrorMessageDepositNonPositiveAmount,
                    new OperationDetailsDto(
                        amount: 0,
                        senderUserId: senderUserId));
            }

            var senderUser = _unitOfWork.UserRepository.GetById(senderUserId);

            senderUser.Balance += longAmount;
            _unitOfWork.TransactionRepository.Add(new Transaction(
                senderUserId: senderUserId,
                receiverUserId: null,
                amount: longAmount,
                transactionType: TransactionType.Deposit));
            _unitOfWork.Save();

            // when amount == 3.1482 for example, long amount will be equal to 314, resultAmount will be equal to 3.14
            var resultAmount = CastFromLong(longAmount);

            return ResultDto.Success(
                new OperationDetailsDto(
                    amount: amount,
                    senderUserId: senderUserId));
        }

        public ResultDto Withdraw(Guid senderUserId, double amount)
        {
            long longAmount = CastFromDouble(amount);

            if (longAmount <= 0)
            {
                return ResultDto.Error(ErrorMessageWithdrawNonPositiveAmount,
                    new OperationDetailsDto(
                        amount: 0,
                        senderUserId: senderUserId));
            }

            var senderUser = _unitOfWork.UserRepository.GetById(senderUserId);

            if (senderUser.Balance < longAmount)
            {
                return ResultDto.Error(ErrorMessageWithdrawNotEnoughMoney,
                    new OperationDetailsDto(
                        amount: 0,
                        senderUserId: senderUserId));
            }

            senderUser.Balance -= longAmount;
            _unitOfWork.TransactionRepository.Add(new Transaction(
                senderUserId: senderUserId, 
                receiverUserId: null, 
                amount: longAmount, 
                transactionType: TransactionType.Withdraw));
            _unitOfWork.Save();

            var resultAmount = CastFromLong(longAmount);

            return ResultDto.Success(
                new OperationDetailsDto(
                    amount: amount,
                    senderUserId: senderUserId));
        }

        public ResultDto Transfer(Guid senderUserId, Guid receiverUserId, double amount)
        {
            long longAmount = CastFromDouble(amount);

            var receiverUser = _unitOfWork.UserRepository.GetById(receiverUserId);

            if (receiverUser == null)
            {
                return ResultDto.Error(ErrorMessageTransferWhenReceiverUserNotFound,
                    new OperationDetailsDto(
                        amount: 0,
                        senderUserId: senderUserId,
                        receiverUserId: receiverUserId));
            }

            if (longAmount <= 0)
            {
                return ResultDto.Error(ErrorMessageTransferNonPositiveAmount, 
                    new OperationDetailsDto(
                        amount: 0,
                        senderUserId: senderUserId,
                        receiverUserId: receiverUserId));
            }

            var senderUser = _unitOfWork.UserRepository.GetById(senderUserId);

            if (senderUser.Balance < longAmount)
            {
                return ResultDto.Error(ErrorMessageTransferNotEnoughMoney,
                    new OperationDetailsDto(
                        amount: 0,
                        senderUserId: senderUserId,
                        receiverUserId: receiverUserId));
            }

            receiverUser.Balance += longAmount;
            senderUser.Balance -= longAmount;
            _unitOfWork.TransactionRepository.Add(new Transaction(
                senderUserId: senderUserId,
                receiverUserId: receiverUserId,
                amount: longAmount,
                transactionType: TransactionType.Transfer));
            _unitOfWork.Save();

            var resultAmount = CastFromLong(longAmount);

            return ResultDto.Success(
                new OperationDetailsDto(
                    amount: amount, 
                    senderUserId: senderUserId, 
                    receiverUserId: receiverUserId));
        }
    }
}
