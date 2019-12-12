using System;

namespace SampleBank.Domain.Services
{
    public class AccountNotFoundException : ApplicationException
    {
        public AccountNotFoundException(string accountNumber)
            : base($"Account number {accountNumber} not found.")
        {
        }
    }
}