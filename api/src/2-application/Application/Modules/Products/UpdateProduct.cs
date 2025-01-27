using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Contracts.Requests.Common;
using Resto.Application.Common.Extensions;
using Resto.Application.Common.Persistence;
using Resto.Common.Enumerations;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Modules.Products;

public static class UpdateProduct
{
    public class Request : UpdateRequest<Guid>, IRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool MultipleToppingsAllowed { get; set; }

        public Guid CategoryId { get; set; }
        public IEnumerable<Guid> ToppingIds { get; set; } = new List<Guid>();
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

            RuleForEach(r => r.ToppingIds)
                .NotEmpty().WithErrorCode(ErrorCode.Invalid)
                .MustAsync(dbContext.ToppingExistsByIdAsync).WithErrorCode(ErrorCode.NotFound);
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
            _logger.LogDebug("Updating product with id {ProductId}", request.Id);

            var product = await _dbContext
                .Products
                // include many-to-many table, otherwise updates to existing relations will be duplicated
                .Include(p => p.Toppings)
                .SingleAsync(p => p.Id == request.Id, cancellationToken);
            _logger.LogDebug("Fetched product to update from database");

            product.Name = request.Name;
            product.Price = request.Price;
            product.MultipleToppingsAllowed = request.MultipleToppingsAllowed;
            product.CategoryId = request.CategoryId;
            product.Toppings = request.ToppingIds
                .Select(id => new ProductTopping { ToppingId = id })
                .ToList();
            _logger.LogDebug("Mapped update request to entity");

            // filter ProductTopping objects that are not present in the request
            var toppingsToDelete = product.Toppings
                .Where(pt => request.ToppingIds.All(tid => pt.ToppingId != tid))
                .ToList();
            toppingsToDelete.ForEach(pt => product.Toppings.Remove(pt));
            // map ID's in request that don't exist as ProductTopping to new relations 
            // and add to list
            var newToppings = request.ToppingIds
                .Where(tid => product.Toppings.All(pt => pt.ToppingId != tid))
                .Select(tid => new ProductTopping { ToppingId = tid })
                .ToList();
            newToppings.ForEach(pt => product.Toppings.Add(pt));
            _logger.LogDebug("Updated related toppings");

            await _dbContext.SaveChangesAsync();
            _logger.LogDebug("Persisted changes to database");
        }
    }
}