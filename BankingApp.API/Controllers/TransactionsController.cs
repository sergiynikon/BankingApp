using System;
using BankingApp.API.Extensions;
using BankingApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsService _transactionsService;

        public TransactionsController(ITransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
        }

        [HttpGet]
        [Route("Current")]
        public IActionResult GetUserTransactions()
        {
            return Ok(_transactionsService.GetUserTransactions(this.GetCurrentUserId()));
        }

        [HttpGet]
        [Route("{userId}")]
        //TODO: add authorization by role [Authorize(Roles = "admin")]
        public IActionResult GetUserTransactions(Guid userId)
        {
            var result = _transactionsService.GetUserTransactions(userId);
            
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}