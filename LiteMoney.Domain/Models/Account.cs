namespace LiteMoney.Domain.Models;

public class Account : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public string? NameCurrency { get; set; }
    public string Icon { get; set; } = null!;
    public string IconColor { get; set; } = null!;
    // public decimal MonthExpensive { get; set; }
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
}
