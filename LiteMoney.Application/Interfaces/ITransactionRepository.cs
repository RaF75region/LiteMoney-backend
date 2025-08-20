using System.Threading;
using System.Threading.Tasks;
using LiteMoney.Domain.Models;

namespace LiteMoney.Application.Interfaces;

public interface ITransactionRepository : IRepository<Transaction>
{
    public Task<Transaction?> GetByIdAsync(int id, CancellationToken ct = default);
}