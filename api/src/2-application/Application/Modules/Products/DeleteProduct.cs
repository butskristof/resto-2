using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Extensions;
using Resto.Application.Common.Persistence;
using Resto.Common.Enumerations;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Modules.Products;

public static class DeleteProduct
{
	public class Request : IRequest
	{
		public Guid ProductId { get; set; }
	}

	internal class Validator : AbstractValidator<Request>
	{
		public Validator(IAppDbContext dbContext)
		{
			RuleFor(r => r.ProductId)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithErrorCode(ErrorCode.Required)
				.MustAsync(dbContext.ProductExistsByIdAsync).WithErrorCode(ErrorCode.NotFound);
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
			_logger.LogDebug("Deleting product with id {ProductId}", request.ProductId);

			var product = new Product {Id = request.ProductId};
			_dbContext.Products.Remove(product);
			await _dbContext.SaveChangesAsync();
			_logger.LogDebug("Removed product from database");
		}
	}
}