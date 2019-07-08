using BankingApp.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using BankingApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BankingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("Current")]
        public IActionResult GetCurrentUser()
        {
            return Ok(_userService.GetUser(this.GetCurrentUserId()));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("All")]
        public IActionResult GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }
    }
}