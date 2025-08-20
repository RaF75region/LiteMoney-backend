using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LiteMoney.Domain.Models;

namespace LiteMoney.Application.Services;

public interface ITransactionService
{
    Task<Transaction?> CreateAsync(Transaction transaction, int accountId, Category category,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
