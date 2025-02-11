using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Extensions;
using Resto.Application.Common.Persistence;
using Resto.Common.Enumerations;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Modules.Toppings;

public static class DeleteTopping
{
	public sealed record Request(Guid ToppingId) : IRequest;

	internal sealed class Validator : AbstractValidator<Request>
	{
		public Validator(IAppDbContext dbContext)
		{
			RuleFor(r => r.ToppingId)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithErrorCode(ErrorCode.Required)
				.MustAsync(dbContext.ToppingExistsByIdAsync).WithErrorCode(ErrorCode.NotFound);
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
			_logger.LogDebug("Deleting topping with id {ToppingId}", request.ToppingId);

			var topping = new Topping {Id = request.ToppingId};
			_dbContext.Toppings.Remove(topping);
			await _dbContext.SaveChangesAsync();
			_logger.LogDebug("Removed topping from database");
		}
	}
}