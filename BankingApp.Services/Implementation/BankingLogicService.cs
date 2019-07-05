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
        private const int CentValue = 100;
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
            return (value / CentValue);
        }

        public OperationDetailsDto Deposit(double amount, Guid senderUserId)
        {
            if (amount <= 0)
            {
                return OperationDetailsDto.Error("Can not open deposit with negative amount");
            }

            _unitOfWork.UserRepository.GetById(senderUserId).Balance += CastFromDouble(amount);

            return OperationDetailsDto.Success(amount);
        }

        public OperationDetailsDto Withdraw(long amount, Guid id)
        {
            var senderUser = _unitOfWork.UserRepository.GetById(id);
            if (senderUser.Balance < amount)
            {
                return OperationDetailsDto.Error("Can not withdraw money, not enough money");
            }

            return OperationDetailsDto.Success(amount);
        }

        public OperationDetailsDto Transfer(Guid senderUserId, Guid receiverUserId, long amount)
        {
            var senderUser = _unitOfWork.UserRepository.GetById(senderUserId);

            var receiverUser = _unitOfWork.UserRepository.GetByEmail(receiverUserEmail);
            if (receiverUser == null)
            {
                return OperationDetailsDto.Error("Can not find receiver user");
            }

            if (senderUser.Balance < amount)
            {
                return OperationDetailsDto.Error("Can not send money, not enough money");
            }

            return OperationDetailsDto.Success(senderUserEmail, receiverUserEmail, amount);
        }
    }
}
