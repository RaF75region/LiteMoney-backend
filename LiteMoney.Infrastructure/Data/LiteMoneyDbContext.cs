using LiteMoney.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LiteMoney.Infrastructure.Data;

public class LiteMoneyDbContext : DbContext
{
    public LiteMoneyDbContext(DbContextOptions<LiteMoneyDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Currency> Currencies => Set<Currency>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
