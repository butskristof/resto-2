using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Extensions;
using Resto.Application.Common.Persistence;
using Resto.Common.Enumerations;

namespace Resto.Application.Modules.Categories;

public static class DeleteCategory
{
	public class Request : IRequest
	{
		public Guid CategoryId { get; set; }
	}

	internal class Validator : AbstractValidator<Request>
	{
		public Validator()
		{
			RuleFor(r => r.CategoryId)
				.NotEmpty().WithErrorCode(ErrorCode.Required);
			// TODO existing category id
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

		public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
		{
			_logger.LogDebug("Deleting category with id {CategoryId}", request.CategoryId);
			
			_logger.LogDebug("Fetching category to delete");
			var category = await _dbContext
				.Categories
				.SingleAsync(c => c.Id == request.CategoryId, cancellationToken);
			// TODO cascade/restrict?
			
			_logger.LogDebug("Deleting category from database and saving changes");
			_dbContext.Categories.Remove(category);
			await _dbContext.SaveChangesAsync();
			_logger.LogDebug("Saved changes to database");

			return Unit.Value;
		}
	}
}