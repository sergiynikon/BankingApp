using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BankingApp.Data;
using BankingApp.Data.UnitOfWork;
using BankingApp.DataTransfer;
using BankingApp.Services.Helpers;
using BankingApp.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BankingApp.Services.Implementation
{
    public class AuthenticateService : IAuthenticateService
    {
        private static readonly string DefaultClaimsType = "Id";

        private static readonly string ErrorMessageIncorrectLogin = "Incorrect login!";
        private static readonly string ErrorMessageIncorrectPassword = "Incorrect password!";
        private static readonly string ErrorMessageCanNotGetIdentity = "Can not get identity";

        private readonly IUnitOfWork _unitOfWork;

        public AuthenticateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ResultDto GetIdentityToken(LoginDto identity)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(identity);
            if (!Validator.TryValidateObject(identity, context, results, validateAllProperties: true))
            {
                return ResultDto.Error("Model is invalid", identity);
            }

            var user = _unitOfWork.UserRepository.GetByLogin(identity.Login);

            if (user == null)
            {
                return ResultDto.Error(ErrorMessageIncorrectLogin, identity);
            }

            if (_unitOfWork.UserRepository.VerifyPassword(user.Id, identity.Password) == false)
            {
                return ResultDto.Error(ErrorMessageIncorrectPassword, identity);
            }

            var claimsIdentity = GetClaimsIdentity(user.Id);

            if (claimsIdentity == null)
            {
                return ResultDto.Error(ErrorMessageCanNotGetIdentity);
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

            return ResultDto.Success(
                new
                {
                    encodedJwt = encodedJwt
                });
        }

        public ResultDto RegisterUser(RegisterDto identity)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(identity);
            if (!Validator.TryValidateObject(identity, context, results, validateAllProperties: true))
            {
                return ResultDto.Error("Model is invalid", identity);
            }

            if (_unitOfWork.UserRepository.UserLoginExists(identity.Login))
            {
                return ResultDto.Error($"login {identity.Login} already exists!");
            }

            if (_unitOfWork.UserRepository.UserEmailExists(identity.Email))
            {
                return ResultDto.Error($"email {identity.Email} already taken!");
            }

            var user = identity.ConvertToUser();

            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.Save();

            return ResultDto.Success(identity);
        }

        private ClaimsIdentity GetClaimsIdentity(Guid userId)
        {
            var claims = new List<Claim>
            {
                new Claim(DefaultClaimsType, userId.ToString())
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