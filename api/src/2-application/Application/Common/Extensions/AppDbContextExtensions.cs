using Microsoft.EntityFrameworkCore;
using Resto.Application.Common.Persistence;

namespace Resto.Application.Common.Extensions;

internal static class AppDbContextExtensions
{
	#region Categories

	/// <summary>
	/// Verify that a given name is not currently used by a Category in the database, except for the Category
	/// with the given ID (so updating and not changing the name is possible).
	/// By default, the comparison is case-insensitive.
	/// </summary>
	internal static Task<bool> CategoryNameIsUniqueAsync(this IAppDbContext context, string name,
		Guid? categoryId = null, CancellationToken cancellationToken = default)
			=> context
				.Categories
				.AsNoTracking()
				.AllAsync(c => c.Id == categoryId || c.Name != name, cancellationToken);
	
	internal static Task<bool> CategoryExistsByIdAsync(this IAppDbContext context, Guid id,
		CancellationToken cancellationToken)
			=> context
				.Categories
				.AsNoTracking()
				.AnyAsync(c => c.Id == id, cancellationToken);
	
	/// <summary>
	/// Verify that a given color is not currently used by a Category in the database, except for the Category
	/// with the given ID (so updating and not changing the color is possible).
	/// By default, the comparison is case-insensitive.
	/// </summary>
	internal static Task<bool> CategoryColorIsUniqueAsync(this IAppDbContext context, string color,
		Guid? categoryId = null, CancellationToken cancellationToken = default)
			=> context
				.Categories
				.AsNoTracking()
				.AllAsync(c => c.Id == categoryId || c.Color != color, cancellationToken);

	#endregion

	#region Toppings

	

	#endregion
}