using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Contracts.Requests.Common;
using Resto.Application.Common.Contracts.Responses.Common;
using Resto.Application.Common.Contracts.Responses.Products;
using Resto.Application.Common.Extensions;
using Resto.Application.Common.Mapping;
using Resto.Application.Common.Persistence;

namespace Resto.Application.Modules.Toppings;

public static class GetToppings
{
    public sealed record Request : PagedRequest, IRequest<Response>;

    public sealed class Response : PagedResponse<ToppingDto>;

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
            _logger.LogDebug("Get toppings: page {Page} w/ page size {PageSize}",
                request.Page, request.PageSize);

            var toppingsQuery = _dbContext
                .Toppings
                .AsNoTracking()
                .OrderBy(e => e.Name)
                .AsQueryable();

            var result = await toppingsQuery
                .MapToToppingDto()
                .GetPagedAsync(request.Page, request.PageSize, cancellationToken: cancellationToken);
            _logger.LogDebug("Fetched mapped toppings from database");

            return result.MapToTypedResponse<ToppingDto, Response>();
        }
    }
}