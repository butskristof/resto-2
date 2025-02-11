using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resto.Domain.Entities.Products;

namespace Resto.Persistence.Configuration;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder
			.Property(p => p.Id)
			.ValueGeneratedOnAdd();
		
		builder
			.Property(p => p.Name)
			.IsRequired();
		builder
			.HasIndex(p => p.Name)
			.IsUnique();

		builder
			.HasOne(p => p.Category)
			.WithMany()
			.HasForeignKey(p => p.CategoryId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}