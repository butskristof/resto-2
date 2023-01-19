namespace Resto.Application.Common.Persistence;

public interface IAppDbContext
{
	Task<int> SaveChangesAsync();
	// EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}