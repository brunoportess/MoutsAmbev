using Ambev.DeveloperEvaluation.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id);

            builder.Property(i => i.ProductId).IsRequired();
            builder.Property(i => i.ProductTitle).HasMaxLength(200);
            builder.Property(i => i.Quantity).IsRequired();
            builder.Property(i => i.UnitPrice).HasColumnType("numeric(18,2)");
            builder.Property(i => i.DiscountPercent).HasColumnType("numeric(18,2)");
            builder.Property(i => i.Total).HasColumnType("numeric(18,2)");
        }
    }
}
