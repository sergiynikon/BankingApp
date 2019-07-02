using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace Services.Interfaces
{
    public interface IAuthenticateService
    {
        bool Login(LoginDTO identity);
    }
}
