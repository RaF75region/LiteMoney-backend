using System.Linq;
using LiteMoney.Application.Interfaces;
using Microsoft.AspNetCore.Builder;
using Nager.Country;

namespace LiteMoney.Infrastructure.GroupMaps;

public class CurrencyGroupMap : IEndpointGroup
{
    public void Map(WebApplication app)
    {
        var group = app.MapGroup("/currencies");

        group.MapGet("/", () =>
        {
            var provider = new CountryProvider();
            var currencies = provider.GetCountries()
                .SelectMany(c => c.Currencies)
                .GroupBy(c => c.ISO4217Code)
                .Select(g => new { Code = g.Key, Name = g.First().Name })
                .OrderBy(x => x.Code)
                .ToList();
            return Results.Ok(currencies);
        });
    }
}
