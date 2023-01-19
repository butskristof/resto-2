using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resto.Domain.Entities.Products;

namespace Resto.Persistence.Configuration;

internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder
			.Property(c => c.Id)
			.ValueGeneratedOnAdd();
		
		builder
			.Property(c => c.Name)
			.IsRequired();
		builder
			.HasIndex(c => c.Name)
			.IsUnique();
	}
}