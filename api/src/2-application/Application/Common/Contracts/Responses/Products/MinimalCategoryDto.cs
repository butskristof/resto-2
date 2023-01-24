using Resto.Application.Common.Mapping;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Common.Contracts.Responses.Products;

public abstract class CategoryDtoBase
{
	public string Name { get; set; }
}

public class MinimalCategoryDto : CategoryDtoBase, IMapFrom<Category>
{
}

public class CategoryDto : CategoryDtoBase, IMapFrom<Category>
{
	public Guid Id { get; set; }
	public DateTime? LastModifiedOn { get; set; }
}