using FastEndpoints;
using LiteMoney.Domain.Models;

namespace LiteMoney.Api.Requests;

public sealed record AccountRequest(
    int Id,
    string Name,
    decimal Balance,
    string NameCurrency,
    string Icon,
    string IconColor);

public sealed record DeleteRequest(
    [property: RouteParam][property:BindFrom("id")] int Id);

public sealed record UpdateRequest(
    [property: RouteParam] int Id,
    string Name,
    decimal Balance,
    string NameCurrency,
    string Icon,
    string IconColor);

public record AccountCreateRequest(string Name, decimal Balance,  string? NameCurrency, string Icon, string IconColor);