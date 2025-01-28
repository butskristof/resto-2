using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Contracts.Responses.Products;
using Resto.Application.Common.Extensions;
using Resto.Application.Common.Mapping;
using Resto.Application.Common.Persistence;
using Resto.Common.Enumerations;
using Resto.Common.Exceptions;

namespace Resto.Application.Modules.Toppings;

public static class GetTopping
{
	public class Request : IRequest<ToppingDto>
	{
		public Guid ToppingId { get; set; }
	}

	internal class Validator : AbstractValidator<Request>
	{
		public Validator()
		{
			RuleFor(r => r.ToppingId)
				.NotEmpty().WithErrorCode(ErrorCode.Required);
		}
	}

	internal class Handler : IRequestHandler<Request, ToppingDto>
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

		public async Task<ToppingDto> Handle(Request request, CancellationToken cancellationToken)
		{
			_logger.LogDebug("Getting topping with id {ToppingId}", request.ToppingId);
			
			var topping = await _dbContext
				.Toppings
				.AsNoTracking()
				.MapToToppingDto()
				.SingleOrDefaultAsync(t => t.Id == request.ToppingId, cancellationToken)
				?? throw new NotFoundException($"Could not find topping with id {request.ToppingId}");
			_logger.LogDebug("Fetched mapped topping from database");
			
			return topping;
		}
	}
}