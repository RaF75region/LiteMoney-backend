using FastEndpoints;
using Nager.Country;

namespace LiteMoney.Api.Endpoints.Currency;

static class CarrencyRoutes
{
    public const string Base = "/currency";
}

public class Get : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get(CarrencyRoutes.Base);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var provider = new CountryProvider();
        var currencies = provider.GetCountries()
            .SelectMany(c => c.Currencies)
            .GroupBy(c => c.IsoCode)
            .Select(g => new { Code = g.Key, Name = g.First().Name, Symbol = g.First().Symbol })
            .OrderBy(x => x.Code)
            .ToList();
        await Send.OkAsync(currencies, cancellationToken);
    }
}