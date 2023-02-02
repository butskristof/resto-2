using AutoMapper;
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
	public class Request : UpdateRequest<Guid>, IRequest
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
	}

	internal class Validator : AbstractValidator<Request>
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

	internal class Handler : IRequestHandler<Request>
	{
		#region construction

		private readonly ILogger<Handler> _logger;
		private readonly IAppDbContext _dbContext;
		private readonly IMapper _mapper;

		public Handler(ILogger<Handler> logger, IAppDbContext dbContext, IMapper mapper)
		{
			_logger = logger;
			_dbContext = dbContext;
			_mapper = mapper;
		}

		#endregion

		public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
		{
			_logger.LogDebug("Updating topping with id {ToppingId}", request.Id);

			var topping = await _dbContext
				.Toppings
				.SingleAsync(t => t.Id == request.Id, cancellationToken);
			_logger.LogDebug("Fetched topping to update from database");

			_mapper.Map(request, topping);
			_logger.LogDebug("Mapped update request to entity");
			await _dbContext.SaveChangesAsync();
			_logger.LogDebug("Persisted changes to database");

			return Unit.Value;
		}
	}
}