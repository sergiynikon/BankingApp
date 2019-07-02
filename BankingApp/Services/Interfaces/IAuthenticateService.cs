using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using DTO;

namespace Services.Interfaces
{
    public interface IAuthenticateService
    {
        string GetIdentityToken(LoginDTO identity);
        User GetUserIdentity(string login, string password);
        User GetUserByLogin(string login);
        void RegisterUser(RegisterDTO identity);
        User GetUserByEmail(string email);
    }
}
