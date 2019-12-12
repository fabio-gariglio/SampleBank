using System.Threading.Tasks;
using SampleBank.Domain.Models;

namespace SampleBank.Domain.Services
{
    public interface IAccountService
    {
        Task<Account> CreateAsync(string name);

        Task<Account> GetByNumberAsync(string number);

        Task<Account> CloseAsync(string number);

        Task<Account> TransferAsync(Transfer transfer);
    }
}