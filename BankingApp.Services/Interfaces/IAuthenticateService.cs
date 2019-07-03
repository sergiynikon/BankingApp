using System;
using System.Collections.Generic;
using System.Security.Claims;
using BankingApp.Data.Entities;
using BankingApp.DataTransfer;

namespace BankingApp.Services.Interfaces
{
    public interface IAuthenticateService
    {
        string GetIdentityToken(LoginDTO identity);
        User GetUserIdentity(string login, string password);
        User GetUserByLogin(string login);
        void RegisterUser(RegisterDTO identity);
        User GetUserByEmail(string email);
        Guid GetUserId(IEnumerable<Claim> claims);
    }
}
