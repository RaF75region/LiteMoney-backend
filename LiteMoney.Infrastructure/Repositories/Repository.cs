using LiteMoney.Application.Interfaces;
using LiteMoney.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LiteMoney.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly LiteMoneyDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(LiteMoneyDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await _dbSet.FindAsync(new object?[] { id }, cancellationToken);

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbSet.ToListAsync(cancellationToken);

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        => await _dbSet.AddAsync(entity, cancellationToken);

    public void Update(T entity) => _dbSet.Update(entity);

    public void Remove(T entity) => _dbSet.Remove(entity);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);
}
