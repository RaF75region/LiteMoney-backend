using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LiteMoney.Application.Interfaces;
using LiteMoney.Domain.Models;
using LiteMoney.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LiteMoney.Infrastructure.Repositories;

public class AccountRepository : RepositoryBase<Account>, IAccountRepository
{
    private readonly LiteMoneyDbContext _ctx;

    public AccountRepository(LiteMoneyDbContext ctx) : base(ctx)
    {
        _ctx = ctx;
    }

    public async Task<List<Account>> GetByUserIdAsync(string userId, CancellationToken ct = default) =>
        await _ctx.Accounts.Where(a => a.UserId == userId).ToListAsync(ct);

    public async Task<Account?> GetByIdAsync(int id, CancellationToken ct = default) =>
        await _ctx.Accounts.FirstOrDefaultAsync(a => a.Id == id, ct);
}