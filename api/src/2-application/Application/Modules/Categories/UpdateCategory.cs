using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Contracts.Requests.Common;
using Resto.Application.Common.Extensions;
using Resto.Application.Common.Persistence;
using Resto.Common.Enumerations;
using Resto.Common.Extensions;

namespace Resto.Application.Modules.Categories;

public static class UpdateCategory
{
	public sealed record Request(
		Guid Id,
		string Name,
		string Color,
		DateTimeOffset? LastModifiedOn
	) : IUpdateRequest<Guid>, IRequest;

	internal sealed class Validator : AbstractValidator<Request>
	{
		public Validator(IAppDbContext context)
		{
			RuleFor(r => r.Id)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithErrorCode(ErrorCode.Required)
				.MustAsync(context.CategoryExistsByIdAsync).WithErrorCode(ErrorCode.NotFound);
			
			RuleFor(r => r.Name)
				.Cascade(CascadeMode.Stop)
				.NotEmpty()
				.WithErrorCode(ErrorCode.Required)
				.MustAsync((request, name, ct) => context.CategoryNameIsUniqueAsync(name, request.Id, ct))
				.WithErrorCode(ErrorCode.NotUnique);
			
			RuleFor(r => r.Color)
				.Cascade(CascadeMode.Stop)
				.NotEmpty()
				.WithErrorCode(ErrorCode.Required)
				.HexColor()
				.WithErrorCode(ErrorCode.Invalid)
				.MustAsync((request, color, ct) => context.CategoryColorIsUniqueAsync(color, request.Id, ct))
				.WithErrorCode(ErrorCode.NotUnique);
		}
	}

	internal sealed class Handler : IRequestHandler<Request>
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
			_logger.LogDebug("Updating category with id {CategoryId}", request.Id);
			
			var category = await _dbContext
				.Categories
				.SingleAsync(c => c.Id == request.Id, cancellationToken);
			_logger.LogDebug("Fetched category to update from database");

			category.Name = request.Name;
			category.Color = request.Color;
			_logger.LogDebug("Mapped update request to entity");
			
			await _dbContext.SaveChangesAsync();
			_logger.LogDebug("Persisted changes to database");
		}
	}
}