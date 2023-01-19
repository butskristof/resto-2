using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resto.Application.Modules.Categories;

namespace Resto.Api.Controllers;

public class CategoriesController : ApiControllerBase
{
	public CategoriesController(ISender mediator) : base(mediator) {}

	[HttpPost]
	public async Task<ActionResult<CreateCategory.Response>> CreateCategory([FromBody] CreateCategory.Request request)
		=> await Mediator.Send(request);

	[HttpDelete("{CategoryId:guid}")]
	public async Task<IActionResult> DeleteCategory([FromRoute] DeleteCategory.Request request)
	{
		await Mediator.Send(request);
		return NoContent();
	}
}