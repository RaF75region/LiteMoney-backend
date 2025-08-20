using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LiteMoney.Domain.Models;

namespace LiteMoney.Application.Interfaces;

public interface IAccountRepository : IRepository<Account>
{
    Task<List<Account>> GetByUserIdAsync(string userId, CancellationToken ct = default);
    Task<Account?> GetByIdAsync(int id, CancellationToken ct = default);
}