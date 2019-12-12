using System;

namespace SampleBank.Domain.Services
{
    public class AccountTransferException : ApplicationException
    {
        public AccountTransferException(string accountNumber, string reason)
            : base($"Unable to transfer from account number {accountNumber}: {reason}")
        {
        }
    }
}