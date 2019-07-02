using System;
using Data.Repositories.Interfaces;

namespace Data.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        int Save();
    }
}