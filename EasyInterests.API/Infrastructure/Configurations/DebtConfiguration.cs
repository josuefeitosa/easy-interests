using EasyInterests.API.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyInterests.API.Infrastructure.Configurations
{
  public class DebtConfiguration : IEntityTypeConfiguration<Debt>
  {
    public void Configure(EntityTypeBuilder<Debt> builder)
    {
      builder.HasKey(x => x.Id);

      builder.Property(x => x.Description).HasColumnName("description").IsRequired();
      builder.Property(x => x.OriginalValue).HasColumnName("original_value").IsRequired();
      builder.Property(x => x.RecalculatedValue).HasColumnName("recalculation_value").IsRequired();
      builder.Property(x => x.DueDate).HasColumnName("due_date").IsRequired();
      builder.Property(x => x.InterestCalcDate).HasColumnName("recalculation_date").IsRequired();
      builder.Property(x => x.ParcelsQty).HasColumnName("parcel_qty").IsRequired();
      builder.Property(x => x.InterestType).HasColumnName("interest_type").IsRequired();
      builder.Property(x => x.InterestInterval).HasColumnName("interest_interval").IsRequired();
      builder.Property(x => x.InterestPercentage).HasColumnName("interest_percentage").IsRequired();
      builder.Property(x => x.NegotiatorComissionPercentage).HasColumnName("negotiator_comission_percent").IsRequired();
      builder.Property(x => x.Paid).HasColumnName("paid");
      builder.Property(x => x.CustomerId).HasColumnName("customer_id").IsRequired();
      builder.Property(x => x.NegotiatorId).HasColumnName("negotiator_id");

      builder
        .HasOne(x => x.Customer)
        .WithMany(x => x.DebtsAsCustomer)
        .HasForeignKey(x => x.CustomerId)
        .OnDelete(DeleteBehavior.Cascade)
        .IsRequired();

      builder
        .HasOne(x => x.Negotiator)
        .WithMany(x => x.DebtsAsNegotiator)
        .HasForeignKey(x => x.NegotiatorId)
        .OnDelete(DeleteBehavior.Cascade)
        .IsRequired();

      builder
        .HasMany(x => x.Parcels)
        .WithOne(x => x.Debt)
        .HasForeignKey(x => x.DebtId)
        .OnDelete(DeleteBehavior.Cascade)
        .IsRequired();

      builder.ToTable("debts");
    }
  }
}
