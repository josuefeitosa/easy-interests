using EasyInterests.API.Application.Models;
using EasyInterests.API.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EasyInterests.API.Infrastructure
{
    public class EasyInterestsDBContext : DbContext
    {
      public EasyInterestsDBContext(DbContextOptions<EasyInterestsDBContext> options) : base(options)
      { }

      public DbSet<User> Users { get; set; }
      public DbSet<Debt> Debts { get; set; }
      public DbSet<DebtParcel> DebtParcels { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new DebtConfiguration());
        modelBuilder.ApplyConfiguration(new DebtParcelConfiguration());
      }
    }
}
