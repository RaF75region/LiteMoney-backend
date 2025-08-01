using System.Threading;
using LiteMoney.Application.Interfaces;
using LiteMoney.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace LiteMoney.Infrastructure.GroupMaps;

public class AccountGroupMap : IEndpointGroup
{
    public void Map(WebApplication app)
    {
        var group = app.MapGroup("/accounts");

        group.MapGet("/", async (IRepository<Account> repo, CancellationToken ct) =>
            Results.Ok(await repo.GetAllAsync(ct)));

        group.MapGet("/{id:int}", async (int id, IRepository<Account> repo, CancellationToken ct) =>
            await repo.GetByIdAsync(id, ct) is Account account
                ? Results.Ok(account)
                : Results.NotFound());

        group.MapPost("/", async (Account account, IRepository<Account> repo, CancellationToken ct) =>
        {
            await repo.AddAsync(account, ct);
            await repo.SaveChangesAsync(ct);
            return Results.Created($"/accounts/{account.Id}", account);
        });

        group.MapDelete("/{id:int}", async (int id, IRepository<Account> repo, CancellationToken ct) =>
        {
            var account = await repo.GetByIdAsync(id, ct);
            if (account is null) return Results.NotFound();
            repo.Remove(account);
            await repo.SaveChangesAsync(ct);
            return Results.NoContent();
        });
    }
}
