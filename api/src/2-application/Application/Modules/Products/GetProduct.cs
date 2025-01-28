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

namespace Resto.Application.Modules.Products;

public static class GetProduct
{
	public class Request : IRequest<ProductDto>
	{
		public Guid ProductId { get; set; }
	}

	internal class Validator : AbstractValidator<Request>
	{
		public Validator()
		{
			RuleFor(r => r.ProductId)
				.NotEmpty().WithErrorCode(ErrorCode.Required);
		}
	}

	internal class Handler : IRequestHandler<Request, ProductDto>
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

		public async Task<ProductDto> Handle(Request request, CancellationToken cancellationToken)
		{
			_logger.LogDebug("Getting product with ID {ProductId}", request.ProductId);

			var product = await _dbContext
				.Products
				.AsNoTracking()
				.MapToProductDto()
				.SingleOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken)
				?? throw new NotFoundException($"Could not find product with id {request.ProductId}");
			_logger.LogDebug("Fetched mapped product from database");

			return product;
		}
	}
}