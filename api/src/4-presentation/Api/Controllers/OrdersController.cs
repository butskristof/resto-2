using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resto.Application.Modules.Orders;

namespace Resto.Api.Controllers;

public class OrdersController : ApiControllerBase
{
	public OrdersController(ISender mediator) : base(mediator) {}

	[HttpGet]
	public async Task<ActionResult<GetOrders.Response>> GetOrders([FromQuery] GetOrders.Request request)
		=> await Mediator.Send(request);

	[HttpPost]
	public async Task<ActionResult<CreateOrder.Response>> CreateOrder([FromBody] CreateOrder.Request request)
		=> await Mediator.Send(request);

	[HttpPost("{OrderId:guid}/print")]
	public async Task<IActionResult> PrintOrderTicket([FromBody] PrintOrderTicket.Request request)
	{
		await Mediator.Send(request);
		return NoContent();
	}

	[HttpGet("stats")]
	public async Task<ActionResult<GetOrderStatistics.Response>> GetOrderStatistics(
		[FromQuery] GetOrderStatistics.Request request)
		=> await Mediator.Send(request);
}