using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resto.Domain.Entities.Products;

namespace Resto.Persistence.Configuration;

internal class CategoryConfiguration : BaseEntityConfiguration<Category>
{
	// TODO
	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder
			.Property(c => c.Name)
			.IsRequired();
		builder
			.HasIndex(c => c.Name)
			.IsUnique();
	}
}