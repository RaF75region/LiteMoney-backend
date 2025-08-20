using System.Security.Claims;
using FastEndpoints;
using LiteMoney.Api.Requests;
using LiteMoney.Application.Services;

namespace LiteMoney.Api.Endpoints;

public class Delete(IAccountService accountService) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete($"{AccountsRoutes.Base}/{{id}}");
        Summary(s =>
        {
            s.Summary = "Удаление аккаунта по ID";
            s.Description = "Удаляет аккаунт по маршруту /accounts/{id}";
            s.Params["id"] = "ID аккаунта для удаления";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    { 
        int accountId = Route<int>("id");
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        await accountService.DeleteAsync(accountId, userId, ct);
        await Send.OkAsync(cancellation:ct);
    }
}