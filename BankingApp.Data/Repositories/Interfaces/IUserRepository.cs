using BankingApp.Data.Entities;

namespace BankingApp.Data.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByLogin(string login);
        User GetByEmail(string email);

        bool UserLoginExists(string login);
        bool UserEmailExists(string email);
    }
}