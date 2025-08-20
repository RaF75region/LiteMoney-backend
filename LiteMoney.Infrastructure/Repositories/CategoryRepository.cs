using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LiteMoney.Application.Interfaces;
using LiteMoney.Domain.Models;
using LiteMoney.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LiteMoney.Infrastructure.Repositories;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    private readonly LiteMoneyDbContext _ctx;

    public CategoryRepository(LiteMoneyDbContext ctx) : base(ctx)
    {
        _ctx = ctx;
    }

    public async Task<List<Category>> GetByUserIdAsync(string userId, CancellationToken ct = default) =>
        await _ctx.Categories.Where(c => c.UserId == userId).ToListAsync(ct);

    public async Task<Category?> GetByIdAsync(int id, CancellationToken ct = default) =>
        await _ctx.Categories.FirstOrDefaultAsync(c => c.Id == id, ct);
}