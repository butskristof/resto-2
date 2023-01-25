using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Contracts.Requests.Common;
using Resto.Application.Common.Extensions;
using Resto.Application.Common.Persistence;
using Resto.Common.Enumerations;

namespace Resto.Application.Modules.Products;

public static class UpdateProduct
{
	public class Request : UpdateRequest<Guid>, IRequest
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public bool MultipleToppingsAllowed { get; set; }

		public Guid CategoryId { get; set; }
	}

	internal class Validator : AbstractValidator<Request>
	{
		public Validator(IAppDbContext dbContext)
		{
			RuleFor(r => r.Id)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithErrorCode(ErrorCode.Required)
				.MustAsync(dbContext.ProductExistsByIdAsync).WithErrorCode(ErrorCode.NotFound);

			RuleFor(r => r.Name)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithErrorCode(ErrorCode.Required)
				.MustAsync((request, name, ct) => dbContext.ProductNameIsUniqueAsync(name, request.Id, ct))
				.WithErrorCode(ErrorCode.NotUnique);
			
			RuleFor(r => r.Price)
				.Cascade(CascadeMode.Stop)
				.GreaterThanOrEqualTo(0)
				.WithErrorCode(ErrorCode.Invalid);
			
			RuleFor(r => r.CategoryId)
				.NotEmpty()
				.WithErrorCode(ErrorCode.Required)
				.MustAsync(dbContext.CategoryExistsByIdAsync).WithErrorCode(ErrorCode.NotFound);
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
			_logger.LogDebug("Updating product with id {ProductId}", request.Id);

			var product = await _dbContext
				.Products
				.SingleAsync(p => p.Id == request.Id, cancellationToken);
			_logger.LogDebug("Fetched product to update from database");

			_mapper.Map(request, product);
			_logger.LogDebug("Mapped update request to entity");
			await _dbContext.SaveChangesAsync();
			_logger.LogDebug("Persisted changes to database");

			return Unit.Value;
		}
	}
}