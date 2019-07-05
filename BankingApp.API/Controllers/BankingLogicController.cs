using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingApp.API.Extensions;
using BankingApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BankingApp.API.Controllers
{
    [Route("api/Operation")]
    [ApiController]
    [Authorize]
    public class BankingLogicController : ControllerBase
    {
        private readonly IBankingLogicService _bankingLogicService;

        public BankingLogicController(IBankingLogicService bankingLogicService)
        {
            _bankingLogicService = bankingLogicService;
        }

        [HttpPost]
        [Route("Deposit")]
        public IActionResult Deposit([FromBody] double amount)
        {

            var result = _bankingLogicService.Deposit(this.GetCurrentUserId(), amount);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("Withdraw")]
        public IActionResult Withdraw([FromBody] double amount)
        {

            var result = _bankingLogicService.Withdraw(this.GetCurrentUserId(), amount);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("Transfer")]
        public IActionResult Transfer([FromBody] Guid receiverUserId, double amount)
        {
            var result = _bankingLogicService.Transfer(this.GetCurrentUserId(), receiverUserId, amount);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}