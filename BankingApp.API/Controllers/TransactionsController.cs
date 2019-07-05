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
        public IActionResult GetUserTransactions()
        {
            //TODO: return object 
            return Ok(new
            {
                receivedTransactions = _transactionsService.GetReceivedTransactions(this.GetCurrentUserId()),
                sentTransactions = _transactionsService.GetSentTransactions(this.GetCurrentUserId())
            });
        }
    }
}