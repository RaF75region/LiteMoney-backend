using System.Threading;
using System.Threading.Tasks;
using FastEndpoints;
using LiteMoney.Application.Services;

namespace LiteMoney.Api.Endpoints.Transaction;

public class DeleteTransactionEndpoint : EndpointWithoutRequest
{
    private readonly ITransactionService _service;

    public DeleteTransactionEndpoint(ITransactionService service)
    {
        _service = service;
    }

    public override void Configure()
    {
        Delete("/transactions/{id:int}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<int>("id");
        var deleted = await _service.DeleteAsync(id, ct);
        if (deleted)
        {
            await SendNoContentAsync(ct);
        }
        else
        {
            await SendNotFoundAsync(ct);
        }
    }
}
