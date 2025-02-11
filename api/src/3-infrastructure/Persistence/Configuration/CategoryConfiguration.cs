using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resto.Common.Constants;
using Resto.Domain.Entities.Products;

namespace Resto.Persistence.Configuration;

internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
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

		builder
			.Property(c => c.Color)
			.IsRequired()
			.HasMaxLength(DomainConstants.HexColorStringLength);
		builder
			.HasIndex(c => c.Color)
			.IsUnique();
	}
}