using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LiteMoney.Domain.Models;

namespace LiteMoney.Application.Services;

public interface IAccountSharingService
{
    // Task ShareAccountAsync(int accountId, string ownerId, string sharedWithUserId, CancellationToken cancellationToken = default);
    // Task<IEnumerable<Account>> GetSharedAccountsAsync(string userId, CancellationToken cancellationToken = default);
}
