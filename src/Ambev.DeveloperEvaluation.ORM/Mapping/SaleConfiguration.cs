using Ambev.DeveloperEvaluation.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .ValueGeneratedNever();

        builder.Property(s => s.CustomerId)
            .IsRequired();

        builder.Property(s => s.TotalAmount)
            .HasColumnType("numeric(18,2)");

        builder.OwnsMany<SaleItem>("_items", b =>
        {
            b.WithOwner().HasForeignKey("SaleId");
            b.Property<Guid>("Id").ValueGeneratedNever();
            b.HasKey("Id");

            b.Property(i => i.ProductId).IsRequired();
            b.Property(i => i.ProductTitle).HasMaxLength(200);
            b.Property(i => i.Quantity).IsRequired();
            b.Property(i => i.UnitPrice).HasColumnType("numeric(18,2)");
            b.Property(i => i.DiscountPercent).HasColumnType("numeric(18,2)");
            b.Property(i => i.Total).HasColumnType("numeric(18,2)");

            b.ToTable("SaleItems");
        });
    }
}
