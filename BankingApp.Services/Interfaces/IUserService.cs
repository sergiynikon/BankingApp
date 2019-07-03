using System;
using BankingApp.DataTransfer;

namespace BankingApp.Services.Interfaces
{
    public interface IUserService
    {
        UserDTO GetUser(Guid id);
    }
}