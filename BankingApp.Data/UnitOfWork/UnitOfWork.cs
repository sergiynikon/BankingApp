using BankingApp.Data.Repositories.Implementation;
using BankingApp.Data.Repositories.Interfaces;
using BankingApp.Data.UnitOfWork.Interfaces;

namespace BankingApp.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
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
    }
}