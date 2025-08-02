using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LiteMoney.Domain.Models;

namespace LiteMoney.Application.Services;

public interface IAccountService
{
    Task<IEnumerable<Account>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Account?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Account> CreateAsync(Account account, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
