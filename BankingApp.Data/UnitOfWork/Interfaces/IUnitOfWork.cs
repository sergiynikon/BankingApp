using BankingApp.Data.Repositories.Interfaces;

namespace BankingApp.Data.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        int Save();
    }
}