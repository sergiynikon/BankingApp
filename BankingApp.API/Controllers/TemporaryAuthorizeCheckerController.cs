using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.API.Controllers
{
    /// <summary>
    /// this class created only for check whether authorization works correctly
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TemporaryAuthorizeCheckerController : ControllerBase
    {
        // GET api/temporaryauthorizechecker
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("you are authorized!");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("you are All!");
        }
    }
}
