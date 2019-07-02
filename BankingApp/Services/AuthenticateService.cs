﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Data;
using Data.Entities;
using Data.UnitOfWork.Interfaces;
using DTO;
using Microsoft.IdentityModel.Tokens;
using Services.Helpers;
using Services.Interfaces;

namespace Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public AuthenticateService(IMapper mapper, DataContext context, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _context = context;
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