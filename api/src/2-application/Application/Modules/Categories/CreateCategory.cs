using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Extensions;
using Resto.Application.Common.Persistence;
using Resto.Common.Enumerations;
using Resto.Domain.Entities.Products;
using ValidationException = Resto.Application.Common.Exceptions.ValidationException;

namespace Resto.Application.Modules.Categories;

public static class CreateCategory
{
	public class Request : IRequest<Response>
	{
		public string Name { get; set; }
	}

	public record Response(Guid Id);
	
	// validator
	internal class Validator : AbstractValidator<Request>
	{
		public Validator(IAppDbContext context)
		{
			RuleFor(e => e.Name)
				.NotEmpty()
				.WithErrorCode(ErrorCode.Required)
				.MustAsync((name, ct) => context.CategoryNameIsUniqueAsync(name, null, ct))
				.WithErrorCode(ErrorCode.NotUnique);
		}
	}

	internal class Handler : IRequestHandler<Request, Response>
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

		public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
		{
			_logger.LogDebug("Adding new category");

			var category = _mapper.Map<Category>(request);
			_logger.LogDebug("Mapped request to entity type");
			
			_dbContext.Categories.Add(category);
			await _dbContext.SaveChangesAsync();
			_logger.LogDebug("Persisted new category to database");

			return new Response(category.Id);
		}
	}
}