using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingApp.Data.UnitOfWork.Interfaces;
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

        public OperationDetailsDto Deposit(Guid senderUserId, double amount)
        {
            long longAmount = CastFromDouble(amount);

            if (longAmount <= 0)
            {
                return OperationDetailsDto.Error(senderUserId, ErrorMessageDepositNonPositiveAmount);
            }

            _unitOfWork.UserRepository.GetById(senderUserId).Balance += CastFromDouble(longAmount);

            // when amount == 3.1482 for example, long amount will be equal to 314, resultAmount will be equal to 3.14
            var resultAmount = CastFromLong(longAmount);

            return OperationDetailsDto.Success(senderUserId, resultAmount);
        }

        public OperationDetailsDto Withdraw(Guid senderUserId, double amount)
        {
            long longAmount = CastFromDouble(amount);

            if (longAmount <= 0)
            {
                return OperationDetailsDto.Error(senderUserId, ErrorMessageWithdrawNonPositiveAmount);
            }

            var senderUser = _unitOfWork.UserRepository.GetById(senderUserId);
            if (senderUser.Balance < longAmount)
            {
                return OperationDetailsDto.Error(senderUserId, ErrorMessageWithdrawNotEnoughMoney);
            }

            senderUser.Balance -= CastFromDouble(longAmount);

            var resultAmount = CastFromLong(longAmount);

            return OperationDetailsDto.Success(senderUserId, resultAmount);
        }

        public OperationDetailsDto Transfer(Guid senderUserId, Guid receiverUserId, double amount)
        {
            long longAmount = CastFromDouble(amount);

            var receiverUser = _unitOfWork.UserRepository.GetById(receiverUserId);
            if (receiverUser == null)
            {
                return OperationDetailsDto.Error(senderUserId, receiverUserId, ErrorMessageTransferWhenReceiverUserNotFound);
            }

            if (longAmount <= 0)
            {
                return OperationDetailsDto.Error(senderUserId, ErrorMessageTransferNonPositiveAmount);
            }

            var senderUser = _unitOfWork.UserRepository.GetById(senderUserId);

            if (senderUser.Balance < longAmount)
            {
                return OperationDetailsDto.Error(senderUserId, receiverUserId, ErrorMessageTransferNotEnoughMoney);
            }

            receiverUser.Balance += longAmount;
            senderUser.Balance -= longAmount;

            var resultAmount = CastFromLong(longAmount);

            return OperationDetailsDto.Success(senderUserId, receiverUserId, resultAmount);
        }
    }
}
