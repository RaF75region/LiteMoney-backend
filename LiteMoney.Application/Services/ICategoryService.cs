using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LiteMoney.Domain.Models;

namespace LiteMoney.Application.Services;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Category> CreateAsync(Category category, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
