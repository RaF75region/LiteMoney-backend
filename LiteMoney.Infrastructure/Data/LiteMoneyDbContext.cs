using LiteMoney.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LiteMoney.Infrastructure.Data;

public class LiteMoneyDbContext : IdentityDbContext<ApplicationUser>
{
    public LiteMoneyDbContext(DbContextOptions<LiteMoneyDbContext> options) : base(options)
    {
    }

    public DbSet<Currency> Currencies => Set<Currency>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<SharedAccount> SharedAccounts => Set<SharedAccount>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
