using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resto.Application.Common.Contracts.Responses.Products;
using Resto.Application.Modules.Categories;
using Resto.Domain.Entities.Products;

namespace Resto.Api.Controllers;

public class CategoriesController : ApiControllerBase
{
	public CategoriesController(ISender mediator) : base(mediator) {}

	[HttpGet]
	public async Task<ActionResult<GetCategories.Response>> GetCategories([FromQuery] GetCategories.Request request)
		=> await Mediator.Send(request);

	[HttpPost]
	public async Task<ActionResult<CreateCategory.Response>> CreateCategory([FromBody] CreateCategory.Request request)
		=> await Mediator.Send(request);

	[HttpGet("{CategoryId:guid}")]
	public async Task<ActionResult<FullCategoryDto>> GetCategory([FromRoute] GetCategory.Request request)
		=> await Mediator.Send(request);

	[HttpPut("{CategoryId:guid}")]
	public async Task<IActionResult> UpdateCategory([FromRoute] Guid CategoryId, 
		[FromBody] UpdateCategory.Request request)
	{
		await Mediator.Send(request);
		return NoContent();
	}

	[HttpDelete("{CategoryId:guid}")]
	public async Task<IActionResult> DeleteCategory([FromRoute] DeleteCategory.Request request)
	{
		await Mediator.Send(request);
		return NoContent();
	}
}