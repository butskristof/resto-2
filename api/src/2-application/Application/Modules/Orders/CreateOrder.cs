using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Resto.Application.Modules.Orders;

public static class CreateOrder
{
	public class Request : IRequest<Response>
	{
		public IEnumerable<OrderLine> OrderLines { get; set; }
		// TODO discount

		public class OrderLine
		{
			public Guid ProductId { get; set; }
			public IEnumerable<Guid> ToppingIds { get; set; } = new List<Guid>();
			public int Quantity { get; set; }
		}
	}

	public record Response(Guid id);
	
	internal class Validator : AbstractValidator<Request> {}

	internal class Handler : IRequestHandler<Request, Response>
	{
		#region construction

		private readonly ILogger<Handler> _logger;

		public Handler(ILogger<Handler> logger)
		{
			_logger = logger;
		}

		#endregion

		public Task<Response> Handle(Request request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}