namespace Resto.Application.Common.Contracts.Responses.Products;

public class MinimalCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
}

public class CategoryDto : MinimalCategoryDto
{
    public DateTime? LastModifiedOn { get; set; }
}