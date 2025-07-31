using LiteMoney.Application.Interfaces;
using LiteMoney.Domain.Models;

namespace LiteMoney.Infrastructure.GroupMaps;

public class TransactionGroupMap : IEndpointGroup
{
    public void Map(WebApplication app)
    {
        var group = app.MapGroup("/transactions");

        group.MapGet("/", async (IRepository<Transaction> repo, CancellationToken ct) =>
            Results.Ok(await repo.GetAllAsync(ct)));

        group.MapGet("/{id:int}", async (int id, IRepository<Transaction> repo, CancellationToken ct) =>
            await repo.GetByIdAsync(id, ct) is Transaction transaction
                ? Results.Ok(transaction)
                : Results.NotFound());

        group.MapPost("/", async (
            Transaction transaction,
            IRepository<Transaction> transactionRepo,
            IRepository<Account> accountRepo,
            IRepository<Category> categoryRepo,
            CancellationToken ct) =>
        {
            var account = await accountRepo.GetByIdAsync(transaction.AccountId, ct);
            var category = await categoryRepo.GetByIdAsync(transaction.CategoryId, ct);
            if (account is null || category is null)
                return Results.BadRequest();

            await transactionRepo.AddAsync(transaction, ct);

            account.Balance += category.Type == CategoryType.Expense
                ? -transaction.Amount
                : transaction.Amount;

            accountRepo.Update(account);

            await transactionRepo.SaveChangesAsync(ct);
            return Results.Created($"/transactions/{transaction.Id}", transaction);
        });

        group.MapDelete("/{id:int}", async (
            int id,
            IRepository<Transaction> transactionRepo,
            IRepository<Account> accountRepo,
            IRepository<Category> categoryRepo,
            CancellationToken ct) =>
        {
            var transaction = await transactionRepo.GetByIdAsync(id, ct);
            if (transaction is null) return Results.NotFound();

            var account = await accountRepo.GetByIdAsync(transaction.AccountId, ct);
            var category = await categoryRepo.GetByIdAsync(transaction.CategoryId, ct);
            if (account is null || category is null)
                return Results.BadRequest();

            transactionRepo.Remove(transaction);

            account.Balance += category.Type == CategoryType.Expense
                ? transaction.Amount
                : -transaction.Amount;

            accountRepo.Update(account);

            await transactionRepo.SaveChangesAsync(ct);
            return Results.NoContent();
        });
    }
}
