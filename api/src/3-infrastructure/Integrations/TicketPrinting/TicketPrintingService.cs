using Microsoft.Extensions.Logging;
using Resto.Common.Integrations.TicketPrinting;
using Resto.Common.Integrations.TicketPrinting.Models;

namespace Resto.Infrastructure.Integrations.TicketPrinting;

internal class TicketPrintingService : ITicketPrintingService
{
	#region construction

	private readonly ILogger<TicketPrintingService> _logger;

	public TicketPrintingService(ILogger<TicketPrintingService> logger)
	{
		_logger = logger;
	}

	#endregion
	
	public Task PrintOrderTicketAsync(OrderTicketData data, CancellationToken cancellationToken = default)
	{
		_logger.LogDebug("Printing ticket, brrrrrrrrrr");
		return Task.CompletedTask;
	}
}