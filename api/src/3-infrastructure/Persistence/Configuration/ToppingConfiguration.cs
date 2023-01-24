using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resto.Domain.Entities.Products;

namespace Resto.Persistence.Configuration;

internal class ToppingConfiguration : IEntityTypeConfiguration<Topping>
{
	public void Configure(EntityTypeBuilder<Topping> builder)
	{
		builder
			.Property(t => t.Id)
			.ValueGeneratedOnAdd();

		builder
			.Property(t => t.Name)
			.IsRequired();
		builder
			.HasIndex(t => t.Name)
			.IsUnique();
	}
}