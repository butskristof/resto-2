using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resto.Application.Common.Contracts.Responses.Products;
using Resto.Application.Modules.Products;

namespace Resto.Api.Controllers;

public class ProductsController : ApiControllerBase
{
	public ProductsController(ISender mediator) : base(mediator) {}

	[HttpGet]
	public async Task<ActionResult<GetProducts.Response>> GetProducts([FromQuery] GetProducts.Request request)
		=> await Mediator.Send(request);
	
	[HttpPost]
	public async Task<ActionResult<CreateProduct.Response>> CreateProduct([FromBody] CreateProduct.Request request)
		=> await Mediator.Send(request);
	
	[HttpGet("{ProductId:guid}")]
	public async Task<ActionResult<ProductDto>> GetProduct([FromRoute] GetProduct.Request request)
		=> await Mediator.Send(request);
	
	[HttpPut("{ProductId:guid}")]
	public async Task<IActionResult> UpdateProduct([FromRoute] Guid ProductId, 
		[FromBody] UpdateProduct.Request request)
	{
		await Mediator.Send(request);
		return NoContent();
	}

	[HttpDelete("{ProductId:guid}")]
	public async Task<IActionResult> DeleteProduct([FromRoute] DeleteProduct.Request request)
	{
		await Mediator.Send(request);
		return NoContent();
	}
}
