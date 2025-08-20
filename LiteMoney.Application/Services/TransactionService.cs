using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using LiteMoney.Application.Interfaces;
using LiteMoney.Domain.Models;

namespace LiteMoney.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;

    public TransactionService(ITransactionRepository transactinoRepository, IAccountRepository _accountRepository)
    {
        _transactionRepository = transactinoRepository;
        _accountRepository = _accountRepository;
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

    public async Task<Transaction?> CreateAsync(Transaction transaction, int accountId, Category category, CancellationToken cancellationToken = default)
    {
        var account = await _accountRepository.GetByIdAsync(accountId, cancellationToken);
        if (account == null)
            throw new NoNullAllowedException("Account not found");

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
        if (account is null)
            return false;

        _transactionRepository.Remove(transaction);

        account.Balance += transaction.Category.Type == CategoryType.Expense
            ? transaction.Amount
            : -transaction.Amount;

        _accountRepository.Update(account);

        await _transactionRepository.SaveChangesAsync(cancellationToken);
        return true;
    }
}
