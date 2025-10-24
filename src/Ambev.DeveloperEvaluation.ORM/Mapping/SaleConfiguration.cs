using Ambev.DeveloperEvaluation.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id);

            builder.Property(s => s.CustomerId).IsRequired();
            builder.Property(s => s.CustomerName).HasMaxLength(200).IsRequired();
            builder.Property(s => s.SaleDate).IsRequired();
            builder.Property(s => s.TotalAmount).HasColumnType("numeric(18,2)");

            builder.HasMany(s => s.SaleItems)
                   .WithOne(i => i.Sale)
                   .HasForeignKey(i => i.SaleId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
