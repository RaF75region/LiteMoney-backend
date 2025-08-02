using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LiteMoney.Domain.Models;

namespace LiteMoney.Application.Services;

public interface ITransactionService
{
    Task<IEnumerable<Transaction>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Transaction?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Transaction?> CreateAsync(Transaction transaction, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
