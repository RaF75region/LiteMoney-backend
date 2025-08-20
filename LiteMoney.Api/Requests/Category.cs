using FastEndpoints;
using LiteMoney.Domain.Models;

namespace LiteMoney.Api.Requests.Category;

public sealed record AddCategoryRequest(string Name, CategoryType Type, string Icon, string IconColor);
public sealed record UpdateCategoryRequest(int Id, string Name, CategoryType Type, string Icon, string IconColor);
public sealed record DeleteCategoryRequest([property:QueryParam]int CategoryId);