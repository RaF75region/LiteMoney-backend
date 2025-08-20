using System.Security.Claims;
using FastEndpoints;
using LiteMoney.Api.Mappers;
using LiteMoney.Api.Requests;
using LiteMoney.Api.Responses.Category;
using LiteMoney.Application.Services;
using LiteMoney.Domain.Models;

namespace LiteMoney.Api.Endpoints;

static class AccountsRoutes
{
    public const string Base = "/accounts";
}

public class Get(IAccountService accountService) : EndpointWithoutRequest<List<AccountResponse>, AccountMapperV2>
{
    public override void Configure()
    {
        Get(AccountsRoutes.Base);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        var accounts = await accountService.GetAllAsync(userId!, ct);
        var result = Map.FromEntities(accounts);
        await Send.OkAsync(result, ct);
    }
}