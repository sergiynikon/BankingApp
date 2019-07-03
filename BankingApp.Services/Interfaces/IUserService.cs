using System;
using BankingApp.DataTransfer;

namespace BankingApp.Services.Interfaces
{
    public interface IUserService
    {
        UserDto GetUser(Guid id);
    }
}