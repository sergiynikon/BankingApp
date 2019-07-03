using System.Linq;
using BankingApp.Data.Entities;
using BankingApp.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public User GetByLogin(string login)
        {
            return Entity.FirstOrDefault(u => u.Login == login);
        }
    }
}