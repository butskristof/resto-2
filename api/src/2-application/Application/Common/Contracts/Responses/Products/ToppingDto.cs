namespace Resto.Application.Common.Contracts.Responses.Products;

public class MinimalToppingDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public class ToppingDto : MinimalToppingDto
{
    public DateTimeOffset? LastModifiedOn { get; set; }
}