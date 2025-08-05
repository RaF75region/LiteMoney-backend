namespace LiteMoney.Domain.Models;

public class SharedAccount
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public Account Account { get; set; } = null!;
    public string OwnerId { get; set; } = string.Empty;
    public ApplicationUser Owner { get; set; } = null!;
    public string SharedWithUserId { get; set; } = string.Empty;
    public ApplicationUser SharedWithUser { get; set; } = null!;
}
