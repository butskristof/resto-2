using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resto.Domain.Entities.Orders;
using Resto.Domain.Entities.Products;

namespace Resto.Persistence.Configuration;

internal class OrderLineToppingConfiguration : IEntityTypeConfiguration<OrderLineTopping>
{
	public void Configure(EntityTypeBuilder<OrderLineTopping> builder)
	{
		builder
			.ToTable("OrderLineToppings");
		
		builder
			.Property(o => o.Id)
			.ValueGeneratedOnAdd();

		builder
			.HasOne<Topping>()
			.WithMany()
			.HasForeignKey(olt => olt.ToppingId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}