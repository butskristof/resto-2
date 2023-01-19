using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Resto.Api.Controllers;

// [Authorize]
[ApiController]
[Route("[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
	protected readonly ISender Mediator;

	protected ApiControllerBase(ISender mediator)
	{
		Mediator = mediator;
	}
}