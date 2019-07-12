using System;
using System.Collections.Generic;
using BankingApp.Data.UnitOfWork;
using BankingApp.DataTransfer;
using BankingApp.Services.Interfaces;

namespace BankingApp.Services.Implementation
{
    public class TransactionsService : ITransactionsService
    {
        private readonly string ErrorMessageUserNotFound = "User not found";

        private readonly IUnitOfWork _unitOfWork;

        public TransactionsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ResultDto GetUserTransactions(Guid userId)
        {
            if (_unitOfWork.UserRepository.GetById(userId) is null)
            {
                return ResultDto.Error(ErrorMessageUserNotFound);
            }

            return ResultDto.Success(
                new
                {
                    ReceivedTransactions = GetReceivedTransactions(userId),
                    SentTransactions = GetSentTransactions(userId)
                });
        }

        private IEnumerable<TransactionViewModelDto> GetSentTransactions(Guid userId)
        {
            var transactions = new List<TransactionViewModelDto>();
            var transactionsFromDatabase = _unitOfWork.TransactionRepository.GetSentTransactionsByUserId(userId);

            foreach (var transaction in transactionsFromDatabase)
            {
                transactions.Add(TransactionViewModelDto.ConvertFromTransaction(transaction));
            }

            return transactions;
        }

        private IEnumerable<TransactionViewModelDto> GetReceivedTransactions(Guid userId)
        {
            var transactions = new List<TransactionViewModelDto>();
            var transactionsFromDatabase = _unitOfWork.TransactionRepository.GetReceivedTransactionsByUserId(userId);

            foreach (var transaction in transactionsFromDatabase)
            {
                transactions.Add(TransactionViewModelDto.ConvertFromTransaction(transaction));
            }

            return transactions;
        }

    }
}