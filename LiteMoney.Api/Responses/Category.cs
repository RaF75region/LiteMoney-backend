using LiteMoney.Domain.Models;

namespace LiteMoney.Api.Responses.Category;

public sealed class CategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public CategoryType Type { get; set; }
    public string? Icon { get; set; }
    public string? IconColor { get; set; }
}

public record class UpdateCategoryResponse(int Id,string Name,
    CategoryType Type, string? Icon, string? IconColor);