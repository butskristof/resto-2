using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resto.Domain.Entities.Orders;

namespace Resto.Persistence.Configuration;

internal class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
{
	public void Configure(EntityTypeBuilder<OrderLine> builder)
	{
		builder
			.ToTable("OrderLines");

		builder
			.Property(o => o.Id)
			.ValueGeneratedOnAdd();

		builder
			.HasOne(ol => ol.Product)
			.WithMany()
			.HasForeignKey(ol => ol.ProductId)
			.OnDelete(DeleteBehavior.Restrict);

		builder
			.HasMany(ol => ol.Toppings)
			.WithOne()
			.HasForeignKey(olt => olt.OrderLineId);
	}
}