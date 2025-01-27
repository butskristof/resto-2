using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Extensions;
using Resto.Application.Common.Persistence;
using Resto.Common.Enumerations;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Modules.Toppings;

public static class CreateTopping
{
	public class Request : IRequest<Response>
	{
		public string Name { get; set; }
		public decimal Price { get; set; }

		internal Topping MapToTopping()
			=> new()
			{
				Name = Name,
				Price = Price,
			};
	}

	public record Response(Guid Id);

	internal class Validator : AbstractValidator<Request>
	{
		public Validator(IAppDbContext dbContext)
		{
			RuleFor(r => r.Name)
				.Cascade(CascadeMode.Stop)
				.NotEmpty()
				.WithErrorCode(ErrorCode.Required)
				.MustAsync((name, ct) => dbContext.ToppingNameIsUniqueAsync(name, null, ct))
				.WithErrorCode(ErrorCode.NotUnique);

			RuleFor(r => r.Price)
				.Cascade(CascadeMode.Stop)
				.GreaterThanOrEqualTo(0)
				.WithErrorCode(ErrorCode.Invalid);
		}
	}

	internal class Handler : IRequestHandler<Request, Response>
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

		public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
		{
			_logger.LogDebug("Adding new topping");

			var topping = request.MapToTopping();
			_logger.LogDebug("Mapped request to entity type");

			_dbContext.Toppings.Add(topping);
			await _dbContext.SaveChangesAsync();
			_logger.LogDebug("Persisted new topping to database");

			return new Response(topping.Id);
		}
	}
}