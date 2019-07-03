using BankingApp.Data.Entities;

namespace BankingApp.Data.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByLogin(string login);

    }
}