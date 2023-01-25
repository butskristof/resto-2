using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resto.Application.Common.Contracts.Responses.Products;
using Resto.Application.Modules.Toppings;

namespace Resto.Api.Controllers;

public class ToppingsController : ApiControllerBase
{
	public ToppingsController(ISender mediator) : base(mediator) {}

	[HttpGet]
	public async Task<ActionResult<GetToppings.Response>> GetToppings([FromQuery] GetToppings.Request request)
		=> await Mediator.Send(request);
	
	[HttpPost]
	public async Task<ActionResult<CreateTopping.Response>> CreateTopping([FromBody] CreateTopping.Request request)
		=> await Mediator.Send(request);
	
	[HttpGet("{ToppingId:guid}")]
	public async Task<ActionResult<ToppingDto>> GetTopping([FromRoute] GetTopping.Request request)
		=> await Mediator.Send(request);
	
	[HttpPut("{ToppingId:guid}")]
	public async Task<IActionResult> UpdateTopping([FromRoute] Guid ToppingId, 
		[FromBody] UpdateTopping.Request request)
	{
		await Mediator.Send(request);
		return NoContent();
	}

	[HttpDelete("{ToppingId:guid}")]
	public async Task<IActionResult> DeleteTopping([FromRoute] DeleteTopping.Request request)
	{
		await Mediator.Send(request);
		return NoContent();
	}
}