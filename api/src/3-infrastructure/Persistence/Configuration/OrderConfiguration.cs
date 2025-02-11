using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resto.Domain.Entities.Orders;

namespace Resto.Persistence.Configuration;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
	public void Configure(EntityTypeBuilder<Order> builder)
	{
		builder
			.Property(o => o.Id)
			.ValueGeneratedOnAdd();

		builder
			.HasMany(o => o.OrderLines)
			.WithOne()
			.HasForeignKey(ol => ol.OrderId);
	}
}