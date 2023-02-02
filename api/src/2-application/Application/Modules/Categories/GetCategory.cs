using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Contracts.Responses.Products;
using Resto.Application.Common.Extensions;
using Resto.Application.Common.Persistence;
using Resto.Common.Enumerations;
using Resto.Common.Exceptions;

namespace Resto.Application.Modules.Categories;

public static class GetCategory
{
	public class Request : IRequest<CategoryDto>
	{
		public Guid CategoryId { get; set; }
	}

	internal class Validator : AbstractValidator<Request>
	{
		public Validator()
		{
			RuleFor(r => r.CategoryId)
				.NotEmpty().WithErrorCode(ErrorCode.Required);
		}
	}

	internal class Handler : IRequestHandler<Request, CategoryDto>
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

		public async Task<CategoryDto> Handle(Request request, CancellationToken cancellationToken)
		{
			_logger.LogDebug("Getting category with ID {CategoryId}", request.CategoryId);

			var category = await _dbContext
				.Categories
				.AsNoTracking()
				.ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
				.SingleOrDefaultAsync(c => c.Id == request.CategoryId, cancellationToken)
				?? throw new NotFoundException($"Could not find category with id {request.CategoryId}");
			_logger.LogDebug("Fetched mapped category from database");
			
			return category;
		}
	}
}