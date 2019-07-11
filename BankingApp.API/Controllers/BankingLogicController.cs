using BankingApp.API.Extensions;
using BankingApp.DataTransfer;
using BankingApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Deposit([FromBody] OperationModelDto model)
        {
            var result = _bankingLogicService.Deposit(this.GetCurrentUserId(), model);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("Withdraw")]
        public IActionResult Withdraw([FromBody] OperationModelDto model)
        {
            var result = _bankingLogicService.Withdraw(this.GetCurrentUserId(), model);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("Transfer")]
        public IActionResult Transfer([FromBody] OperationModelDto model)
        {
            var result = _bankingLogicService.Transfer(this.GetCurrentUserId(), model);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}