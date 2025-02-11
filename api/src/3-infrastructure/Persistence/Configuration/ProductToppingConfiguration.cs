using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resto.Domain.Entities.Products;

namespace Resto.Persistence.Configuration;

internal sealed class ProductToppingConfiguration : IEntityTypeConfiguration<ProductTopping>
{
	public void Configure(EntityTypeBuilder<ProductTopping> builder)
	{
		builder
			.ToTable("ProductToppings");
		
		builder
			.Property(pt => pt.Id)
			.ValueGeneratedOnAdd();

		builder
			.HasOne<Product>()
			.WithMany(p => p.Toppings)
			.HasForeignKey(pt => pt.ProductId);

		builder
			.HasOne(pt => pt.Topping)
			.WithMany()
			.HasForeignKey(pt => pt.ToppingId);
	}
}