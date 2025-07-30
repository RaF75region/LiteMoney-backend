namespace LiteMoney.Domain.Models;

public class Account
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Balance { get; set; }

    public int CurrencyId { get; set; }
    public Currency Currency { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
