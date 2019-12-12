using System.Threading.Tasks;
using SampleBank.Domain.Models;

namespace SampleBank.Domain.Services
{
    public interface IAccountRepository
    {
        Task<Account> GetByAccountNumberAsync(string accountNumber);
        Task SaveAccountAsync(Account account);
    }
}
