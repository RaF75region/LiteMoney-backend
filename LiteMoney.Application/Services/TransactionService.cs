using System;
using System.Collections.Generic;
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
