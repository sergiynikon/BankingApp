using BankingApp.Data.Repositories.Interfaces;

namespace BankingApp.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        int Save();
    }
}