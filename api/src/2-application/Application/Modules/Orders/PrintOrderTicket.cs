using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Extensions;
using Resto.Application.Common.Persistence;
using Resto.Common.Enumerations;
using Resto.Common.Exceptions;
using Resto.Common.Integrations.TicketPrinting;
using Resto.Common.Integrations.TicketPrinting.Models;

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
			RuleFor(r => r.OrderId)
				.NotEmpty().WithErrorCode(ErrorCode.Required);
		}
	}

	internal class Handler : IRequestHandler<Request>
	{
		#region construction

		private readonly ILogger<Handler> _logger;
		private readonly IAppDbContext _dbContext;
		private readonly ITicketPrintingService _ticketPrintingService;
		private readonly IMapper _mapper;

		public Handler(ILogger<Handler> logger, IAppDbContext dbContext, ITicketPrintingService ticketPrintingService, IMapper mapper)
		{
			_logger = logger;
			_dbContext = dbContext;
			_ticketPrintingService = ticketPrintingService;
			_mapper = mapper;
		}

		#endregion

		public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
		{
			_logger.LogDebug("Printing ticket for Order with ID {OrderId}", request.OrderId);

			var order = await _dbContext
				.OrdersBaseQuery(false)
				.ProjectTo<OrderTicketData>(_mapper.ConfigurationProvider)
				.SingleOrDefaultAsync(o => o.Id == request.OrderId,
					cancellationToken: cancellationToken)
				?? throw new NotFoundException($"Could not find product with id {request.OrderId}");
			
			await _ticketPrintingService.PrintOrderTicketAsync(order, cancellationToken);
			
			return Unit.Value;
		}
	}
}