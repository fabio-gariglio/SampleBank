using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleBank.API.Contracts;
using SampleBank.Domain.Models;
using SampleBank.Domain.Services;

namespace SampleBank.API.Controllers
{
    [Route("accounts")]
    public class AccountsController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{accountNumber}")][ActionName(nameof(Get))]
        public async Task<IActionResult> Get(string accountNumber)
        {
            var account = await _accountService.GetByNumberAsync(accountNumber);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(ToAccountResponse(account));
        }

        [HttpPost][ActionName(nameof(Create))]
        public async Task<IActionResult> Create([FromBody] CreateAccountRequest request)
        {
            var account = await _accountService.CreateAsync(request.Name);

            return CreatedAtAction(
                nameof(Get), new {accountNumber = account.Number},
                ToAccountResponse(account)
            );
        }

        [HttpPost("{accountNumber}/close")][ActionName(nameof(Close))]
        public async Task<IActionResult> Close(string accountNumber)
        {
            try
            {
                var account = await _accountService.CloseAsync(accountNumber);

                return Ok(account);
            }
            catch (AccountNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("{accountNumber}/makePayment")]
        [ActionName(nameof(MakePayment))]
        public async Task<IActionResult> MakePayment(string accountNumber, [FromBody] MakePaymentRequest request)
        {
            try
            {
                var transfer = new Transfer
                {
                    SenderAccountNumber = accountNumber,
                    PayeeAccountNumber = request.PayeeAccountNumber,
                    Amount = request.Amount,
                    Reference = request.Reference
                };

                var account = await _accountService.TransferAsync(transfer);

                return Ok(account);
            }
            catch (AccountNotFoundException)
            {
                return NotFound();
            }
        }

        private static AccountResponse ToAccountResponse(Account account)
        {
            return new AccountResponse
            {
                Name = account.Name,
                Balance = account.Balance,
                Number = account.Number
            };
        }
    }
}
