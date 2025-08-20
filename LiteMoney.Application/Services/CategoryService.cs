using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LiteMoney.Application.Interfaces;
using LiteMoney.Domain.Models;

namespace LiteMoney.Application.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<IEnumerable<Category>> GetFromUser(string userId, CancellationToken cancellationToken = default) =>
        await categoryRepository.GetByUserIdAsync(userId, cancellationToken);

    public async Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await categoryRepository.GetByIdAsync(id, cancellationToken);

    public async Task<Category> CreateAsync(Category category, CancellationToken cancellationToken = default)
    {
        await categoryRepository.AddAsync(category, cancellationToken);
        await categoryRepository.SaveChangesAsync(cancellationToken);
        return category;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await categoryRepository.GetByIdAsync(id, cancellationToken);
        if (entity is null) return false;
        categoryRepository.Remove(entity);
        await categoryRepository.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task UpdateAsync(Category category, CancellationToken cancellationToken = default)
    {
        categoryRepository.Update(category);
        await categoryRepository.SaveChangesAsync(cancellationToken);
    }
}
