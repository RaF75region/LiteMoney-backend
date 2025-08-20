using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using LiteMoney.Application.Interfaces;
using LiteMoney.Domain.Models;

namespace LiteMoney.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _repository;

    public AccountService(IAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Account>> GetAllAsync(string userId, CancellationToken cancellationToken = default)
        => (await _repository.GetAllAsync(cancellationToken)).Where(a => a.UserId == userId);

    public async Task<Account?> GetByIdAsync(int id, string userId, CancellationToken cancellationToken = default)
    {
        var account = await _repository.GetByIdAsync(id, cancellationToken);
        return account?.UserId == userId ? account : null;
    }

    public async Task<Account> CreateAsync(Account account, string userId, CancellationToken cancellationToken = default)
    {
        account.UserId = userId;
        await _repository.AddAsync(account, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        return account;
    }

    public async Task<Account?> UpdateAsync(Account account, string userId, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(account.Id, cancellationToken);
        if (entity is null || entity.UserId != userId) return null;

        entity.Name = account.Name;
        entity.Balance = account.Balance;
        entity.NameCurrency = account.NameCurrency;

        _repository.Update(entity);
        await _repository.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<bool> DeleteAsync(int id, string userId, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity is null || entity.UserId != userId) return false;
        _repository.Remove(entity);
        await _repository.SaveChangesAsync(cancellationToken);
        return true;
    }
}
