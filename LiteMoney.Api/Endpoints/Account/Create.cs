using System.Security.Claims;
using FastEndpoints;
using LiteMoney.Api.Mappers;
using LiteMoney.Api.Requests;
using LiteMoney.Api.Responses.Category;
using LiteMoney.Application.Services;
using LiteMoney.Domain.Models;

namespace LiteMoney.Api.Endpoints.Category;

public class Create(IAccountService accountService) : Endpoint<AccountRequest, AccountResponse, AccountMapperV2>
{
    public override void Configure()
    {
        Post(AccountsRoutes.Base);
    }

    public override async Task HandleAsync(AccountRequest request, CancellationToken ct)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        var account = Map.ToEntity(request);
        await accountService.CreateAsync(account, userId, ct);
        await Send.OkAsync(Map.FromEntity(account), cancellation: ct);
    }
}