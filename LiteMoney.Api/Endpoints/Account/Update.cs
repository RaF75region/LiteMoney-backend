using System.Security.Claims;
using LiteMoney.Api.Mappers;
using LiteMoney.Api.Requests;
using FastEndpoints;
using LiteMoney.Domain.Models;

namespace LiteMoney.Api.Endpoints;

public class Update : Endpoint<UpdateRequest, Account, AccountMapper>
{
    public override void Configure()
    {
        Put(AccountsRoutes.Base);
    }

    public override async Task HandleAsync(UpdateRequest r, CancellationToken c)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        var account = Map.ToEntity(r);
        account.UserId = userId;
        await Send.OkAsync(c);
    }
}