using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LiteMoney.Application.Interfaces;
using LiteMoney.Domain.Models;
using LiteMoney.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LiteMoney.Infrastructure.Repositories;

public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
{
    private readonly LiteMoneyDbContext _ctx;

    public TransactionRepository(LiteMoneyDbContext ctx) : base(ctx)
    {
        _ctx = ctx;
    }

    public async Task<List<Transaction>> GetByUserIdAsync(string userId, CancellationToken ct = default) =>
        await _ctx.Transactions.Where(a => a.UserId == userId).ToListAsync(ct);

    public async Task<Transaction?> GetByIdAsync(int id, CancellationToken ct = default) =>
        await _ctx.Transactions.FirstOrDefaultAsync(a => a.Id == id, ct);

    public async Task<List<Transaction>> GetByCategoryId(Category category, string userId, CancellationToken ct = default) =>
        await _ctx.Transactions.Where(t => t.Category == category && t.UserId == userId).ToListAsync(ct);
}