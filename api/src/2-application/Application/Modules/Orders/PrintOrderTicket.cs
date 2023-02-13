using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Resto.Application.Modules.Orders;

public static class PrintOrderTicket
{
	public class Request : IRequest
	{
		public Guid OrderId { get; set; }
	}

	internal class Validator : AbstractValidator<Request>
	{
		public Validator()
		{
			
		}
	}

	internal class Handler : IRequestHandler<Request>
	{
		#region construction

		private readonly ILogger<Handler> _logger;

		public Handler(ILogger<Handler> logger)
		{
			_logger = logger;
		}

		#endregion

		public Task<Unit> Handle(Request request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}