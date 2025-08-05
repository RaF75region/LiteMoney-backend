using System.Security.Claims;
using LiteMoney.Application.Services;
using LiteMoney.Application.Interfaces;
using LiteMoney.Domain.Models;
using LiteMoney.Infrastructure.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace LiteMoney.Infrastructure.GroupMaps;

public class SharingGroupMap : IEndpointGroup
{
    public record ShareRequest(string Email);

    public void Map(WebApplication app)
    {
        var group = app.MapGroup("/sharing");

        group.MapPost("/accounts/{accountId:int}", async (int accountId, ShareRequest request, ClaimsPrincipal principal, UserManager<ApplicationUser> userManager, IAccountSharingService service, IHubContext<SharingHub> hub, CancellationToken ct) =>
        {
            var ownerId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ownerId is null) return Results.Unauthorized();

            var target = await userManager.FindByEmailAsync(request.Email);
            if (target is null) return Results.NotFound();

            await service.ShareAccountAsync(accountId, ownerId, target.Id, ct);
            await hub.Clients.User(target.Id).SendAsync("AccountShared", accountId, ct);

            return Results.Ok();
        });

        group.MapGet("/accounts", async (ClaimsPrincipal principal, IAccountSharingService service, CancellationToken ct) =>
        {
            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Results.Unauthorized();

            var accounts = await service.GetSharedAccountsAsync(userId, ct);
            return Results.Ok(accounts);
        });
    }
}
