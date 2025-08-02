using System.Threading;
using LiteMoney.Application.Interfaces;
using LiteMoney.Application.Services;
using LiteMoney.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace LiteMoney.Infrastructure.GroupMaps;

public class TransactionGroupMap : IEndpointGroup
{
    public void Map(WebApplication app)
    {
        var group = app.MapGroup("/transactions");

        group.MapGet("/", async (ITransactionService service, CancellationToken ct) =>
            Results.Ok(await service.GetAllAsync(ct)));

        group.MapGet("/{id:int}", async (int id, ITransactionService service, CancellationToken ct) =>
            await service.GetByIdAsync(id, ct) is Transaction transaction
                ? Results.Ok(transaction)
                : Results.NotFound());

        group.MapPost("/", async (
            Transaction transaction,
            ITransactionService service,
            CancellationToken ct) =>
        {
            var created = await service.CreateAsync(transaction, ct);
            return created is null
                ? Results.BadRequest()
                : Results.Created($"/transactions/{created.Id}", created);
        });

        group.MapDelete("/{id:int}", async (
            int id,
            ITransactionService service,
            CancellationToken ct) =>
            await service.DeleteAsync(id, ct)
                ? Results.NoContent()
                : Results.NotFound());
    }
}
