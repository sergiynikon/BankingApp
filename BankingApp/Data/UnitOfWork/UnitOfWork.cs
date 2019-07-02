using System;
using System.Threading.Tasks;
using Data.Repositories;
using Data.Repositories.Interfaces;
using Data.UnitOfWork.Interfaces;

namespace Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext _context;
        private IUserRepository _userRepository;
        private ITransactionRepository _transactionRepository;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

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