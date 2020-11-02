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

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
      }
    }
}
