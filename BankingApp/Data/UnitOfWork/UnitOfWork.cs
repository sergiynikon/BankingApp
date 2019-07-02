using System;
using System.Threading.Tasks;
using Data.Repositories;
using Data.Repositories.Interfaces;
using Data.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context = new DataContext();
        private IUserRepository _userRepository;
        private ITransactionRepository _transactionRepository;


        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository = _userRepository ?? new UserRepository(_context);
            }
        }

        public ITransactionRepository TransactionRepository
        {
            get
            {
                return _transactionRepository = _transactionRepository ?? new TransactionRepository(_context);
            }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}