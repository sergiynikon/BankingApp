using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingApp.API.Extensions;
using BankingApp.DataTransfer;
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
        public IActionResult Deposit([FromBody] DepositModelDto model)
        {

            var result = _bankingLogicService.Deposit(this.GetCurrentUserId(), model.Amount);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("Withdraw")]
        public IActionResult Withdraw([FromBody] WithdrawModelDto model)
        {

            var result = _bankingLogicService.Withdraw(this.GetCurrentUserId(), model.Amount);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("Transfer")]
        public IActionResult Transfer([FromBody] TransferModelDto model)
        {
            var result = _bankingLogicService.Transfer(this.GetCurrentUserId(), model.ReceiverUserId, model.Amount);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}