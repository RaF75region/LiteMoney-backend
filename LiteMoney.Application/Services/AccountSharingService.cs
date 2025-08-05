using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LiteMoney.Application.Interfaces;
using LiteMoney.Domain.Models;

namespace LiteMoney.Application.Services;

public class AccountSharingService : IAccountSharingService
{
    private readonly IRepository<SharedAccount> _sharedRepository;
    private readonly IRepository<Account> _accountRepository;

    public AccountSharingService(IRepository<SharedAccount> sharedRepository, IRepository<Account> accountRepository)
    {
        _sharedRepository = sharedRepository;
        _accountRepository = accountRepository;
    }

    public async Task ShareAccountAsync(int accountId, string ownerId, string sharedWithUserId, CancellationToken cancellationToken = default)
    {
        var entity = new SharedAccount
        {
            AccountId = accountId,
            OwnerId = ownerId,
            SharedWithUserId = sharedWithUserId
        };
        await _sharedRepository.AddAsync(entity, cancellationToken);
        await _sharedRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Account>> GetSharedAccountsAsync(string userId, CancellationToken cancellationToken = default)
    {
        var accounts = await _accountRepository.GetAllAsync(cancellationToken);
        var own = accounts.Where(a => a.UserId == userId);
        var sharedIds = (await _sharedRepository.GetAllAsync(cancellationToken))
            .Where(s => s.SharedWithUserId == userId)
            .Select(s => s.AccountId);
        var shared = accounts.Where(a => sharedIds.Contains(a.Id));
        return own.Concat(shared);
    }
}
