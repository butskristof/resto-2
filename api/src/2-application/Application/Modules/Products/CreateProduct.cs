using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Extensions;
using Resto.Application.Common.Mapping;
using Resto.Application.Common.Persistence;
using Resto.Common.Enumerations;

namespace Resto.Application.Modules.Products;

public static class CreateProduct
{
    public sealed record Request(
        string Name,
        decimal Price,
        bool MultipleToppingsAllowed,
        Guid CategoryId
    ) : IRequest<Response>
    {
        public IEnumerable<Guid> ToppingIds { get; init; } = [];
    }

    public sealed record Response(Guid Id);

    internal sealed class Validator : AbstractValidator<Request>
    {
        public Validator(IAppDbContext dbContext)
        {
            RuleFor(r => r.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithErrorCode(ErrorCode.Required)
                .MustAsync((name, ct) => dbContext.ProductNameIsUniqueAsync(name, null, ct))
                .WithErrorCode(ErrorCode.NotUnique);

            RuleFor(r => r.Price)
                .Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo(0)
                .WithErrorCode(ErrorCode.Invalid);

            RuleFor(r => r.CategoryId)
                .NotEmpty()
                .WithErrorCode(ErrorCode.Required)
                .MustAsync(dbContext.CategoryExistsByIdAsync).WithErrorCode(ErrorCode.NotFound);
            RuleForEach(r => r.ToppingIds)
                .NotEmpty().WithErrorCode(ErrorCode.Invalid)
                .MustAsync(dbContext.ToppingExistsByIdAsync).WithErrorCode(ErrorCode.NotFound);
        }
    }

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
            _logger.LogDebug("Adding new product");

            var product = request.MapToProduct();
            _logger.LogDebug("Mapped request to entity type");

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            _logger.LogDebug("Persisted new category to database");

            return new Response(product.Id);
        }
    }
}