using System.Security.Claims;
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
        var group = app.MapGroup("/accounts").RequireAuthorization();

        group.MapGet("/", async (ClaimsPrincipal principal, IAccountService service, CancellationToken ct) =>
        {
            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Results.Unauthorized();

            return Results.Ok(await service.GetAllAsync(userId, ct));
        });

        group.MapGet("/{id:int}", async (int id, ClaimsPrincipal principal, IAccountService service, CancellationToken ct) =>
        {
            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Results.Unauthorized();

            var account = await service.GetByIdAsync(id, userId, ct);
            return account is null ? Results.NotFound() : Results.Ok(account);
        });

        group.MapPost("/", async (Account account, ClaimsPrincipal principal, IAccountService service, CancellationToken ct) =>
        {
            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Results.Unauthorized();

            var created = await service.CreateAsync(account, userId, ct);
            return Results.Created($"/accounts/{created.Id}", created);
        });

        group.MapPut("/{id:int}", async (int id, Account account, ClaimsPrincipal principal, IAccountService service, CancellationToken ct) =>
        {
            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Results.Unauthorized();

            account.Id = id;
            var updated = await service.UpdateAsync(account, userId, ct);
            return updated is null ? Results.NotFound() : Results.Ok(updated);
        });

        group.MapDelete("/{id:int}", async (int id, ClaimsPrincipal principal, IAccountService service, CancellationToken ct) =>
        {
            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Results.Unauthorized();

            return await service.DeleteAsync(id, userId, ct)
                ? Results.NoContent()
                : Results.NotFound();
        });
    }
}
