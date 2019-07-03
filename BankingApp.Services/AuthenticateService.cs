using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using BankingApp.Data;
using BankingApp.Data.Entities;
using BankingApp.Data.UnitOfWork.Interfaces;
using BankingApp.DataTransfer;
using BankingApp.Services.Helpers;
using BankingApp.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BankingApp.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public AuthenticateService(IMapper mapper, DataContext context, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public string GetIdentityToken(LoginDTO identity)
        {
            if (identity == null)
            {
                return null;
            }
            
            var claimsIdentity = GetClaimsIdentity(identity.Login, identity.Password);

            if (claimsIdentity == null)
            {
                return null;
            }

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                notBefore: now,
                claims: claimsIdentity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public User GetUserIdentity(string login, string password)
        {
            return _unitOfWork.UserRepository
                .Find(u => u.Login == login
                           &&
                           u.Password == password)
                .FirstOrDefault();
        }

        public User GetUserByLogin(string login)
        {
            return _unitOfWork.UserRepository.GetByLogin(login);
        }

        public void RegisterUser(RegisterDTO identity)
        {
            var user = _mapper.Map<User>(identity);
            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.Save();
        }

        public User GetUserByEmail(string email)
        {
            return _unitOfWork.UserRepository.Find(u => u.Email == email).SingleOrDefault();
        }

        public Guid GetUserId(IEnumerable<Claim> claims)
        {
            return Guid.Parse(claims.First(c => c.Type == "Id").Value);
        }

        private ClaimsIdentity GetClaimsIdentity(string login, string password)
        {
            var user = _unitOfWork.UserRepository
                .Find(u => u.Login == login && u.Password == password)
                .SingleOrDefault();

            if (user == null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString())
            };

            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(
                    claims,
                    "Token",
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}