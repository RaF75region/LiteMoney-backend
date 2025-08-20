using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using LiteMoney.Application.Services;
using LiteMoney.Domain.Models;

namespace LiteMoney.Api.Endpoints.Transaction;

public class GetTransactionsRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetTransactionsEndpoint : Endpoint<GetTransactionsRequest, IEnumerable<Transaction>>
{
    private readonly ITransactionService _service;

    public GetTransactionsEndpoint(ITransactionService service)
    {
        _service = service;
    }

    public override void Configure()
    {
        Get("/transactions");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetTransactionsRequest req, CancellationToken ct)
    {
        var transactions = await _service.GetPaginatedAsync(req.PageNumber, req.PageSize, ct);
        await SendOkAsync(transactions, ct);
    }
}
