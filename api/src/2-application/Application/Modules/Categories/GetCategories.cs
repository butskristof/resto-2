using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Contracts.Requests.Common;
using Resto.Application.Common.Contracts.Responses.Common;
using Resto.Application.Common.Contracts.Responses.Products;
using Resto.Application.Common.Extensions;
using Resto.Application.Common.Mapping;
using Resto.Application.Common.Persistence;

namespace Resto.Application.Modules.Categories;

public static class GetCategories
{
    public sealed record Request : PagedRequest, IRequest<Response>;

    public sealed class Response : PagedResponse<CategoryDto>;

    internal sealed class Validator : PagedRequestValidator<Request>;

    internal sealed class Handler : IRequestHandler<Request, Response>
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
            _logger.LogDebug("Get categories: page {Page} w/ page size {PageSize}",
                request.Page, request.PageSize);

            var categoriesQuery = _dbContext
                .Categories
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .AsQueryable();

            var result = await categoriesQuery
                .MapToCategoryDto()
                .GetPagedAsync(request.Page, request.PageSize, cancellationToken: cancellationToken);
            _logger.LogDebug("Fetched mapped categories from database");

            return result.MapToTypedResponse<CategoryDto, Response>();
        }
    }
}