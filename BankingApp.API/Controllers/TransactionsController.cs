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
        private readonly IAuthenticateService _authenticateService;

        public TransactionsController(ITransactionsService transactionsService, IAuthenticateService authenticateService)
        {
            _transactionsService = transactionsService;
            _authenticateService = authenticateService;
        }

        [HttpGet]
        public IActionResult GetUserTransactions()
        {
            var userId = _authenticateService.GetUserId(User.Claims);
            return Ok(new
            {
                receivedTransactions = _transactionsService.GetReceivedTransactions(userId),
                sentTransactions = _transactionsService.GetSentTransactions(userId)
            });
        }
    }
}