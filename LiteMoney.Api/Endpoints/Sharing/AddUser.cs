using System.Security.Claims;
using FastEndpoints;
using LiteMoney.Api.Requests;

namespace LiteMoney.Api.Endpoints.Sharing;

public class SharingEndpoint
{
    public const string Base = "/sharing";
}

public class AddUser : Endpoint<SharingRequest>
{
    public override void Configure()
    {
        Post("/sharing");
    }

    public override async Task HandleAsync(SharingRequest request, CancellationToken ct)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        
        
    }
}