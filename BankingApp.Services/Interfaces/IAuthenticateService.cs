using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using BankingApp.Data.Entities;
using BankingApp.DataTransfer;

namespace BankingApp.Services.Interfaces
{
    public interface IAuthenticateService
    {
        string GetIdentityToken(LoginDto identity);
        User GetUserIdentity(string login, string password);
        AuthenticationDetailsDto RegisterUser(RegisterDto identity);
        Guid GetUserId(IEnumerable<Claim> claims);
    }
}
