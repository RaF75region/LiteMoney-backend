using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LiteMoney.Domain.Models;

namespace LiteMoney.Application.Services;

public interface IAccountService
{
    Task<IEnumerable<Account>> GetAllAsync(string userId, CancellationToken cancellationToken = default);
    Task<Account?> GetByIdAsync(int id, string userId, CancellationToken cancellationToken = default);
    Task<Account> CreateAsync(Account account, string userId, CancellationToken cancellationToken = default);
    Task<Account?> UpdateAsync(Account account, string userId, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, string userId, CancellationToken cancellationToken = default);
}
