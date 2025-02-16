using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Persistence;

namespace Resto.Application.Modules.Orders;

public static class DeleteOrders
{
    public sealed record Request : IRequest;

    internal sealed class Handler : IRequestHandler<Request>
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
            _logger.LogWarning("Deleting all orders");

            await _dbContext.Orders
                .ExecuteDeleteAsync(CancellationToken.None);
            _logger.LogInformation("Removed all orders from database");
        }
    }
}