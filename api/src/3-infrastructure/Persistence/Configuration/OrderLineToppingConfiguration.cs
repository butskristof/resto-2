using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resto.Domain.Entities.Orders;

namespace Resto.Persistence.Configuration;

internal sealed class OrderLineToppingConfiguration : IEntityTypeConfiguration<OrderLineTopping>
{
	public void Configure(EntityTypeBuilder<OrderLineTopping> builder)
	{
		builder
			.ToTable("OrderLineToppings");
		
		builder
			.Property(o => o.Id)
			.ValueGeneratedOnAdd();

		builder
			.HasOne(olt => olt.Topping)
			.WithMany()
			.HasForeignKey(olt => olt.ToppingId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}