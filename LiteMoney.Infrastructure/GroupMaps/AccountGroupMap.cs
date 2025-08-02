using System.Threading;
using LiteMoney.Application.Interfaces;
using LiteMoney.Application.Services;
using LiteMoney.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace LiteMoney.Infrastructure.GroupMaps;

public class AccountGroupMap : IEndpointGroup
{
    public void Map(WebApplication app)
    {
        var group = app.MapGroup("/accounts");

        group.MapGet("/", async (IAccountService service, CancellationToken ct) =>
            Results.Ok(await service.GetAllAsync(ct)));

        group.MapGet("/{id:int}", async (int id, IAccountService service, CancellationToken ct) =>
            await service.GetByIdAsync(id, ct) is Account account
                ? Results.Ok(account)
                : Results.NotFound());

        group.MapPost("/", async (Account account, IAccountService service, CancellationToken ct) =>
        {
            var created = await service.CreateAsync(account, ct);
            return Results.Created($"/accounts/{created.Id}", created);
        });

        group.MapDelete("/{id:int}", async (int id, IAccountService service, CancellationToken ct) =>
            await service.DeleteAsync(id, ct)
                ? Results.NoContent()
                : Results.NotFound());
    }
}
