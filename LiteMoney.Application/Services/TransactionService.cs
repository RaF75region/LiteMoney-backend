using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LiteMoney.Application.Interfaces;
using LiteMoney.Domain.Models;

namespace LiteMoney.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly IRepository<Transaction> _transactionRepository;
    private readonly IRepository<Account> _accountRepository;
    private readonly IRepository<Category> _categoryRepository;

    public TransactionService(
        IRepository<Transaction> transactionRepository,
        IRepository<Account> accountRepository,
        IRepository<Category> categoryRepository)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _transactionRepository.GetAllAsync(cancellationToken);

    public async Task<IEnumerable<Transaction>> GetPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var transactions = await _transactionRepository.GetAllAsync(cancellationToken);
        return transactions
            .OrderByDescending(t => t.Date)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    public async Task<Transaction?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await _transactionRepository.GetByIdAsync(id, cancellationToken);

    public async Task<Transaction?> CreateAsync(Transaction transaction, CancellationToken cancellationToken = default)
    {
        var account = await _accountRepository.GetByIdAsync(transaction.AccountId, cancellationToken);
        var category = await _categoryRepository.GetByIdAsync(transaction.CategoryId, cancellationToken);
        if (account is null || category is null)
            return null;

        await _transactionRepository.AddAsync(transaction, cancellationToken);

        account.Balance += category.Type == CategoryType.Expense
            ? -transaction.Amount
            : transaction.Amount;

        _accountRepository.Update(account);

        await _transactionRepository.SaveChangesAsync(cancellationToken);
        return transaction;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var transaction = await _transactionRepository.GetByIdAsync(id, cancellationToken);
        if (transaction is null) return false;

        var account = await _accountRepository.GetByIdAsync(transaction.AccountId, cancellationToken);
        var category = await _categoryRepository.GetByIdAsync(transaction.CategoryId, cancellationToken);
        if (account is null || category is null)
            return false;

        _transactionRepository.Remove(transaction);

        account.Balance += category.Type == CategoryType.Expense
            ? transaction.Amount
            : -transaction.Amount;

        _accountRepository.Update(account);

        await _transactionRepository.SaveChangesAsync(cancellationToken);
        return true;
    }
}
