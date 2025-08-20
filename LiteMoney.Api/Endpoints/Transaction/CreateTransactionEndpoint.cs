using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using LiteMoney.Application.Services;
using LiteMoney.Domain.Models;

namespace LiteMoney.Api.Endpoints.Transaction;

public class CreateTransactionEndpoint : Endpoint<Transaction, Transaction>
{
    private readonly ITransactionService _service;

    public CreateTransactionEndpoint(ITransactionService service)
    {
        _service = service;
    }

    public override void Configure()
    {
        Post("/transactions");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Transaction req, CancellationToken ct)
    {
        var created = await _service.CreateAsync(req, ct);
        if (created is null)
        {
            await SendBadRequestAsync(ct);
            return;
        }

        await SendCreatedAtAsync<GetTransactionByIdEndpoint>(new { id = created.Id }, created, cancellation: ct);
    }
}
