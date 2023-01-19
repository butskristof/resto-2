using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resto.Domain.Common;

namespace Resto.Persistence.Configuration;

internal abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T>
	where T : BaseEntity
{
	public void Configure(EntityTypeBuilder<T> builder)
	{
		builder
			.Property(e => e.Id)
			.ValueGeneratedOnAdd();
	}
}