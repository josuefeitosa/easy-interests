using EasyInterests.API.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyInterests.API.Infrastructure.Configurations
{
  public class DebtParcelConfiguration : IEntityTypeConfiguration<DebtParcel>
  {
    public void Configure(EntityTypeBuilder<DebtParcel> builder)
    {
      builder.HasKey(x => x.Id);

      builder.Property(x => x.Parcel).HasColumnName("parcel").IsRequired();
      builder.Property(x => x.Value).HasColumnName("value").IsRequired();
      builder.Property(x => x.DueDate).HasColumnName("due_date").IsRequired();
      builder.Property(x => x.DebtId).HasColumnName("debt_id").IsRequired();

      builder
        .HasOne(x => x.Debt)
        .WithMany(x => x.Parcels)
        .HasForeignKey(x => x.DebtId)
        .OnDelete(DeleteBehavior.SetNull)
        .IsRequired();

      builder.ToTable("debt_parcels");
    }
  }
}
