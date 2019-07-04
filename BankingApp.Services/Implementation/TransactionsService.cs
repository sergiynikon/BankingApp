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

        public IEnumerable<TransactionViewDto> GetSentTransactions(Guid userId)
        {
            var transactions = new List<TransactionViewDto>();
            var transactionsFromDatabase = _unitOfWork.TransactionRepository.GetSentTransactionsByUserId(userId);
            foreach (var transaction in transactionsFromDatabase)
            {
                transactions.Add(TransactionViewDto.ConvertFromTransaction(transaction));
            }
            return transactions;
        }

        public IEnumerable<TransactionViewDto> GetReceivedTransactions(Guid userId)
        {
            var transactions = new List<TransactionViewDto>();
            var transactionsFromDatabase = _unitOfWork.TransactionRepository.GetReceivedTransactionsByUserId(userId);
            foreach (var transaction in transactionsFromDatabase)
            {
                transactions.Add(TransactionViewDto.ConvertFromTransaction(transaction));
            }
            return transactions;
        }
    }
}