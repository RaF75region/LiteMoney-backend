using System;

namespace LiteMoney.Domain.Models;

public enum CategoryType
{
    Expense,
    Income
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public CategoryType Type { get; set; }
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser? User { get; set; }
}
