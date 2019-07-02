using System;
using Data.Entities;

namespace Data.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByLogin(string login);

    }
}