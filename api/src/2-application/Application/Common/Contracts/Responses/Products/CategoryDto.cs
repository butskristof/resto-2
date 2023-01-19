using Resto.Application.Common.Mapping;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Common.Contracts.Responses.Products;

public class CategoryDto : IMapFrom<Category>
{
	public Guid Id { get; set; }
	public string Name { get; set; }
}