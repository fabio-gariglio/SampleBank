using System.Threading.Tasks;
using SampleBank.Domain.Models;
using SampleBank.Domain.Services;

namespace SampleBank.Infrastructure
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IRepository<Account> _repository;

        public AccountRepository(IRepository<Account> repository)
        {
            this._repository = repository;
        }
        
        public Task<Account> GetByAccountNumberAsync(string accountNumber)
        {
            return _repository.GetByIdAsync(accountNumber);
        }

        public Task SaveAccountAsync(Account account)
        {
            return _repository.SaveAsync(account, account.Number);
        }
    }
}
