using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Extensions;
using Resto.Application.Common.Persistence;
using Resto.Common.Enumerations;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Modules.Categories;

public static class DeleteCategory
{
	public class Request : IRequest
	{
		public Guid CategoryId { get; set; }
	}

	internal class Validator : AbstractValidator<Request>
	{
		public Validator(IAppDbContext dbContext)
		{
			RuleFor(r => r.CategoryId)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithErrorCode(ErrorCode.Required)
				.MustAsync(dbContext.CategoryExistsByIdAsync).WithErrorCode(ErrorCode.NotFound);
			// TODO prevent if still assigned to product
		}
	}

	internal class Handler : IRequestHandler<Request>
	{
		#region construction

		private readonly ILogger<Handler> _logger;
		private readonly IAppDbContext _dbContext;

		public Handler(ILogger<Handler> logger, IAppDbContext dbContext)
		{
			_logger = logger;
			_dbContext = dbContext;
		}

		#endregion

		public async Task Handle(Request request, CancellationToken cancellationToken)
		{
			_logger.LogDebug("Deleting category with id {CategoryId}", request.CategoryId);

			// eliminate fetching the entity to delete from the database first by creating a POCO with just the id
			var category = new Category {Id = request.CategoryId};
			// and instruct the db context to remove that entity
			_dbContext.Categories.Remove(category);
			// the resulting query will remove the row by id
			await _dbContext.SaveChangesAsync();
			_logger.LogDebug("Removed category from database");
		}
	}
}