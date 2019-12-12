using System;
using System.Linq;
using System.Threading.Tasks;
using SampleBank.Domain.Models;

namespace SampleBank.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly Random _random = new Random();

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account> CreateAsync(string name)
        {
            var account = new Account
            {
                Name = name,
                Number = GenerateAccountNumber(),
                Balance = 0,
                Created = DateTime.UtcNow,
                Open = true
            };

            await _accountRepository.SaveAccountAsync(account);

            return account;
        }

        public Task<Account> GetByNumberAsync(string number)
        {
            return _accountRepository.GetByAccountNumberAsync(number);
        }

        public async Task<Account> CloseAsync(string number)
        {
            var account = await _accountRepository.GetByAccountNumberAsync(number);

            if (account == null) throw new AccountNotFoundException(number);

            account.Open = false;

            await _accountRepository.SaveAccountAsync(account);

            return account;
        }

        public async Task<Account> TransferAsync(Transfer transfer)
        {
            var senderAccount = await _accountRepository.GetByAccountNumberAsync(transfer.SenderAccountNumber);

            if (senderAccount == null)
            {
                throw new AccountNotFoundException(transfer.SenderAccountNumber);
            }

            if (!senderAccount.Open)
            {
                throw new AccountTransferException(transfer.SenderAccountNumber, "Account closed");
            }

            if (senderAccount.Balance < )
            {
                throw new AccountTransferException(transfer.SenderAccountNumber, "Account closed");
            }

            var payeeAccount = await _accountRepository.GetByAccountNumberAsync(transfer.PayeeAccountNumber);

            if (payeeAccount == null)
            {
                throw new AccountTransferException(transfer.PayeeAccountNumber, "Payee account not found");
            }

            if (!payeeAccount.Open)
            {
                throw new AccountTransferException(transfer.PayeeAccountNumber, "Payee account closed");
            }

            senderAccount.Balance -= transfer.Amount;

            await _accountRepository.SaveAccountAsync(senderAccount);

            payeeAccount.Balance += transfer.Amount;

            await _accountRepository.SaveAccountAsync(payeeAccount);

            return senderAccount;
        }

        private string GenerateAccountNumber()
        {
            var accountNumberDigits = Enumerable
                .Range(0, 8)
                .Select(x => _random.Next(0, 9).ToString());

            var accountNumber = string.Join(string.Empty, accountNumberDigits);

            return accountNumber;
        }
    }
}
