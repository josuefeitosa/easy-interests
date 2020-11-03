using EasyInterests.API.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyInterests.API.Infrastructure.Configurations
{
  public class UserConfiguration : IEntityTypeConfiguration<User>
  {
    public void Configure(EntityTypeBuilder<User> builder)
    {
      builder.HasKey(x => x.Id);

      builder.Property(x => x.Id).HasColumnName("id").IsRequired();
      builder.Property(x => x.Name).HasColumnName("name").IsRequired();
      builder.Property(x => x.Email).HasColumnName("email").IsRequired();
      builder.Property(x => x.PhoneNumber).HasColumnName("phone_number").IsRequired();
      builder.Property(x => x.Role).HasColumnName("role").IsRequired();

      builder.ToTable("users");
    }
  }
}
