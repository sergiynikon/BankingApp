using BankingApp.DataTransfer;
using BankingApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.API.Controllers
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

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginDto identity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _authenticateService.GetUserIdentity(identity.Login, identity.Password);

            if (userIdentity == null)
            {
                return BadRequest(ResultDto.Error("Incorrect login or password!"));
            }

            var token = _authenticateService.GetIdentityToken(identity);

            return Ok(token);
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterDto identity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _authenticateService.RegisterUser(identity);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}