using Resto.Application.Common.Mapping;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Common.Contracts.Responses.Products;

public class ProductDto : IMapFrom<Product>
{
	public Guid Id { get; set; }
	public DateTime? LastModifiedOn { get; set; }

	public string Name { get; set; }
	public decimal Price { get; set; }
	public bool MultipleToppingsAllowed { get; set; }

	public MinimalCategoryDto Category { get; set; }
}