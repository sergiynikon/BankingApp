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

            var result = _bankingLogicService.Deposit(amount, this.GetCurrentUserId());
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            
        }
    }
}