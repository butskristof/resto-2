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
}