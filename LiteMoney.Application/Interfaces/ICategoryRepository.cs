using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LiteMoney.Domain.Models;

namespace LiteMoney.Application.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<List<Category>> GetByUserIdAsync(string userId, CancellationToken ct = default);
    Task<Category?> GetByIdAsync(int id, CancellationToken ct = default);
}