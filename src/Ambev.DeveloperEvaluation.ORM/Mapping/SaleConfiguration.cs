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
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(s => s.SaleNumber)
            .IsRequired()
            .HasMaxLength(40);

        builder.Property(s => s.CustomerName)
            .HasMaxLength(200);

        builder.Property(s => s.Branch)
            .HasMaxLength(80);

        builder.Property(s => s.TotalAmount)
            .HasColumnType("numeric(18,2)");

        builder.Navigation("_items")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsMany<SaleItem>("_items", b =>
        {
            b.WithOwner().HasForeignKey("SaleId");
            b.Property<Guid>("Id").ValueGeneratedNever();
            b.HasKey("Id");
            b.Property(i => i.ProductId).IsRequired();
            b.Property(i => i.ProductTitle).HasMaxLength(200);
            b.Property(i => i.Quantity).IsRequired();
            b.Property(i => i.UnitPrice).HasColumnType("numeric(18,2)");
            b.Property(i => i.DiscountPercent).HasColumnType("numeric(5,2)");
            b.Property(i => i.Total).HasColumnType("numeric(18,2)");
        });
    }
}
