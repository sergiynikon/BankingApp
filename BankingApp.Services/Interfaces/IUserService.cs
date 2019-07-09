using System;
using System.Collections.Generic;
using BankingApp.DataTransfer;

namespace BankingApp.Services.Interfaces
{
    public interface IUserService
    {
        UserViewModelDto GetUser(Guid id);
        IEnumerable<UserViewModelDto> GetAllUsers();
    }
}