namespace Resto.Application.Common.Contracts.Responses.Products;

public class ProductDto
{
    public Guid Id { get; set; }
    public DateTime? LastModifiedOn { get; set; }

    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool MultipleToppingsAllowed { get; set; }

    public MinimalCategoryDto Category { get; set; }
    public IEnumerable<MinimalToppingDto> Toppings { get; set; }
}