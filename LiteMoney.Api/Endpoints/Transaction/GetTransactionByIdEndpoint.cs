using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using LiteMoney.Application.Services;
using LiteMoney.Domain.Models;

namespace LiteMoney.Api.Endpoints.Transaction;

public class GetTransactionByIdEndpoint : EndpointWithoutRequest<Transaction>
{
    private readonly ITransactionService _service;

    public GetTransactionByIdEndpoint(ITransactionService service)
    {
        _service = service;
    }

    public override void Configure()
    {
        Get("/transactions/{id:int}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<int>("id");
        var transaction = await _service.GetByIdAsync(id, ct);
        if (transaction is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(transaction, ct);
    }
}
