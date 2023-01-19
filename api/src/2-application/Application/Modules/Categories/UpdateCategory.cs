using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Extensions;
using Resto.Application.Common.Persistence;
using Resto.Common.Enumerations;

namespace Resto.Application.Modules.Categories;

public static class UpdateCategory
{
	public class Request : IRequest
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public DateTime? LastModifiedOn { get; set; }
	}

	internal class Validator : AbstractValidator<Request>
	{
		// name: required & unique (w/ lowercase)
		public Validator()
		{
			RuleFor(r => r.Id)
				.NotEmpty().WithErrorCode(ErrorCode.Required);
			// TODO exists
			RuleFor(e => e.Name)
				.NotEmpty().WithErrorCode(ErrorCode.Required);
			// TODO unique
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
			_logger.LogDebug("Updating category with id {CategoryId}", request.Id);
			
			_logger.LogDebug("Fetching category to update");
			var category = await _dbContext
				.Categories
				.SingleAsync(c => c.Id == request.Id, cancellationToken);

			_logger.LogDebug("Mapping request to entity and saving changes to database");
			_mapper.Map(request, category);
			await _dbContext.SaveChangesAsync();
			_logger.LogDebug("Saved changes to database");

			return Unit.Value;
		}
	}
}