using MediatR;

namespace Resto.Api.Controllers;

public class CategoriesController : ApiControllerBase
{
	public CategoriesController(ISender mediator) : base(mediator) {}
	
	
}