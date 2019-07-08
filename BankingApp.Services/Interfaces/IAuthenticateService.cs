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
        ResultDto GetIdentityToken(LoginDto identity);
        ResultDto RegisterUser(RegisterDto identity);
    }
}
