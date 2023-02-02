using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resto.Application.Modules.Orders;

namespace Resto.Api.Controllers;

public class OrdersController : ApiControllerBase
{
	public OrdersController(ISender mediator) : base(mediator) {}
	
	[HttpPost]
	public async Task<ActionResult<CreateOrder.Response>> CreateOrder([FromBody] CreateOrder.Request request)
		=> await Mediator.Send(request);
}