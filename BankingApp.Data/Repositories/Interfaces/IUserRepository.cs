using BankingApp.Data.Entities;

namespace BankingApp.Data.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        bool UserLoginExists(string login);
        bool UserEmailExists(string email);
    }
}