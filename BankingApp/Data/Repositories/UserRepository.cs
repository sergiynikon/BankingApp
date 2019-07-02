using System.Linq;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context, DbSet<User> entity) : base(context, entity)
        {
        }

        public User GetByLogin(string login)
        {
            return Entity.FirstOrDefault(u => u.Login == login);
        }
    }
}