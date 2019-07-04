using System;
using System.Collections.Generic;
using BankingApp.DataTransfer;

namespace BankingApp.Services.Interfaces
{
    public interface IUserService
    {
        UserDto GetUser(Guid id);
        IEnumerable<UserDto> GetAllUsers();
    }
}