using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;

        public AuthenticateController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO identity)
        {
            if (identity == null)
            {
                return BadRequest();
            }

            if (_authenticateService.GetUserByLogin(identity.Login) == null)
            {
                return BadRequest(new { message = "Incorrect login!" });
            }


            var userIdentity = _authenticateService.GetUserIdentity(identity.Login, identity.Password);

            if (userIdentity == null)
            {
                return BadRequest(new { message = "Incorrect password!" });
            }

            var token = _authenticateService.GetIdentityToken(identity);
            if (token == null)
            {
                return BadRequest(new { message = "Can not authorize, please try again later!" });
            }

            return Ok(token);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDTO identity)
        {
            if (identity == null)
            {
                return BadRequest();
            }

            var userByLogin = _authenticateService.GetUserByLogin(identity.Login);
            if (userByLogin != null)
            {
                return BadRequest(new {message = $"User with login '{identity.Login}' already exists!"});
            }

            var userByEmail = _authenticateService.GetUserByEmail(identity.Email);
            if (userByEmail != null)
            {
                return BadRequest(new {message = $"User with email '{identity.Email}' already exists!"});
            }

            //TODO: move magic number '5' to separate settings file
            int minPasswordLength = 5;
            if (identity.Password.Length < minPasswordLength)
            {
                return BadRequest(new {message = $"Password length must be at least {minPasswordLength}"});
            }

            if (identity.Password != identity.ConfirmPassword)
            {
                return BadRequest(new {message = $"Password do not match, please confirm password!"});
            }

            _authenticateService.RegisterUser(identity);

            return Ok();
        }
    }
}