using System;
using System.Collections.Generic;
using BankingApp.DataTransfer;

namespace BankingApp.Services.Interfaces
{
    public interface IUserService
    {
        UserViewDto GetUser(Guid id);
        IEnumerable<UserViewDto> GetAllUsers();
    }
}