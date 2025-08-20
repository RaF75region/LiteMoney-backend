namespace LiteMoney.Api.Responses.Category;

public record AccountResponse(int Id, string Name, decimal Balance,  string? NameCurrency, string Icon, string IconColor);
