using Ambev.DeveloperEvaluation.Domain.Carts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("Carts");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(c => c.UserId)
            .IsRequired();

        builder.Property(c => c.Date)
            .IsRequired();

        builder.OwnsMany<CartProduct>("Products", cb =>
        {
            cb.WithOwner().HasForeignKey("CartId");
            cb.Property<int>("Id");
            cb.HasKey("Id");

            cb.Property(i => i.ProductId).IsRequired();
            cb.Property(i => i.Quantity).IsRequired();
        });

        builder.Navigation(c => c.Products)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
