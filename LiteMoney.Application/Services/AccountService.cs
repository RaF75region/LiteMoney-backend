using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LiteMoney.Application.Interfaces;
using LiteMoney.Domain.Models;

namespace LiteMoney.Application.Services;

public class AccountService : IAccountService
{
    private readonly IRepository<Account> _repository;

    public AccountService(IRepository<Account> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Account>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _repository.GetAllAsync(cancellationToken);

    public async Task<Account?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await _repository.GetByIdAsync(id, cancellationToken);

    public async Task<Account> CreateAsync(Account account, CancellationToken cancellationToken = default)
    {
        await _repository.AddAsync(account, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        return account;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity is null) return false;
        _repository.Remove(entity);
        await _repository.SaveChangesAsync(cancellationToken);
        return true;
    }
}
