using System;
using System.Collections.Generic;
using BankingApp.Data.Entities;
using BankingApp.Data.UnitOfWork.Interfaces;
using BankingApp.Services.Interfaces;

namespace BankingApp.Services.Implementation
{
    public class TransactionsService : ITransactionsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Transaction> GetSentTransactions(Guid userId)
        {
            return _unitOfWork.TransactionRepository.GetSentTransactionsByUserId(userId);
        }

        public IEnumerable<Transaction> GetReceivedTransactions(Guid userId)
        {
            return _unitOfWork.TransactionRepository.GetReceivedTransactionsByUserId(userId);
        }
    }
}