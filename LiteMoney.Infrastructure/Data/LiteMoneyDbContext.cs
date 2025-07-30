using LiteMoney.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LiteMoney.Infrastructure.Data;

public class LiteMoneyDbContext : DbContext
{
    public LiteMoneyDbContext(DbContextOptions<LiteMoneyDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
