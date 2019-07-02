using AutoMapper;
using DTO;
using Services.Interfaces;

namespace Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IMapper _mapper;
        public bool Login(LoginDTO identity)
        {
            
        }
    }
}