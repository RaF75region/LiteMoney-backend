using System;

namespace LiteMoney.Domain.Models;

public enum CategoryType
{
    Expense,
    Income
}

public class Category : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public CategoryType Type { get; set; }
    public string? Icon { get; set; }
    public string? IconColor { get; set; }
    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
}
