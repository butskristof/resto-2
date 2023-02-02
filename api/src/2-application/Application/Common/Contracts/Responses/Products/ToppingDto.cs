using Resto.Application.Common.Mapping;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Common.Contracts.Responses.Products;

public abstract class ToppingDtoBase
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public decimal Price { get; set; }
}

public class MinimalToppingDto : ToppingDtoBase, IMapFrom<Topping> {}

public class ToppingDto : ToppingDtoBase, IMapFrom<Topping>
{
	public DateTime? LastModifiedOn { get; set; }
}
