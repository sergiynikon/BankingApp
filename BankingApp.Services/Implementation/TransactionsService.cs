using System;
using System.Collections.Generic;
using BankingApp.Data.UnitOfWork.Interfaces;
using BankingApp.DataTransfer;
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

        public IEnumerable<TransactionDto> GetSentTransactions(Guid userId)
        {
            var transactions = new List<TransactionDto>();
            var transactionsFromDatabase = _unitOfWork.TransactionRepository.GetSentTransactionsByUserId(userId);
            foreach (var transaction in transactionsFromDatabase)
            {
                transactions.Add(TransactionDto.ConvertFromTransaction(transaction));
            }
            return transactions;
        }

        public IEnumerable<TransactionDto> GetReceivedTransactions(Guid userId)
        {
            var transactions = new List<TransactionDto>();
            var transactionsFromDatabase = _unitOfWork.TransactionRepository.GetReceivedTransactionsByUserId(userId);
            foreach (var transaction in transactionsFromDatabase)
            {
                transactions.Add(TransactionDto.ConvertFromTransaction(transaction));
            }
            return transactions;
        }
    }
}