using System.Linq;
using BankingApp.Data.Entities;
using BankingApp.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Data.Repositories.Implementation
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public bool UserLoginExists(string login)
        {
            return Entity.Any(u => u.Login == login);
        }

        public bool UserEmailExists(string email)
        {
            return Entity.Any(u => u.Email == email);
        }
    }
}