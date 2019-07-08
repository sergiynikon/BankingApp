using System;
using BankingApp.Data.Entities;

namespace BankingApp.Data.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        bool UserLoginExists(string login);
        bool UserEmailExists(string email);
        User GetByLogin(string login);
        bool VerifyPassword(Guid userId, string password);
    }
}