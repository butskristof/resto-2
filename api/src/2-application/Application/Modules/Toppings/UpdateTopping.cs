using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Contracts.Requests.Common;
using Resto.Application.Common.Extensions;
using Resto.Application.Common.Persistence;
using Resto.Common.Enumerations;

namespace Resto.Application.Modules.Toppings;

public static class UpdateTopping
{
	public sealed record Request(
		Guid Id,
		DateTimeOffset? LastModifiedOn,
		string Name,
		decimal Price
	) : IUpdateRequest<Guid>, IRequest;

	internal sealed class Validator : AbstractValidator<Request>
	{
		public Validator(IAppDbContext dbContext)
		{
			RuleFor(r => r.Id)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithErrorCode(ErrorCode.Required)
				.MustAsync(dbContext.ToppingExistsByIdAsync).WithErrorCode(ErrorCode.NotFound);
			
			RuleFor(r => r.Name)
				.Cascade(CascadeMode.Stop)
				.NotEmpty()
				.WithErrorCode(ErrorCode.Required)
				.MustAsync((request, name, ct) => dbContext.ToppingNameIsUniqueAsync(name, request.Id, ct))
				.WithErrorCode(ErrorCode.NotUnique);

			RuleFor(r => r.Price)
				.Cascade(CascadeMode.Stop)
				.GreaterThanOrEqualTo(0)
				.WithErrorCode(ErrorCode.Invalid);
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
			_logger.LogDebug("Updating topping with id {ToppingId}", request.Id);

			var topping = await _dbContext
				.Toppings
				.SingleAsync(t => t.Id == request.Id, cancellationToken);
			_logger.LogDebug("Fetched topping to update from database");

			topping.Name = request.Name;
			topping.Price = request.Price;
			_logger.LogDebug("Mapped update request to entity");
			
			await _dbContext.SaveChangesAsync();
			_logger.LogDebug("Persisted changes to database");
		}
	}
}