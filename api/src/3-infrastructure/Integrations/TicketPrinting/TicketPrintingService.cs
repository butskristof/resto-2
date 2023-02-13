using ESCPOS_NET;
using ESCPOS_NET.Emitters;
using Microsoft.Extensions.Logging;
using Resto.Common.Configuration;
using Resto.Common.Extensions;
using Resto.Common.Integrations.TicketPrinting;
using Resto.Common.Integrations.TicketPrinting.Models;

namespace Resto.Infrastructure.Integrations.TicketPrinting;

internal class TicketPrintingService : ITicketPrintingService
{
	#region construction

	private readonly ILogger<TicketPrintingService> _logger;
	private readonly ITicketPrintingConfiguration _configuration;
	private readonly FilePrinter _printer;

	public TicketPrintingService(ILogger<TicketPrintingService> logger, ITicketPrintingConfiguration configuration)
	{
		_logger = logger;
		_configuration = configuration;
		
		if (_configuration.PrinterAvailable)
		{
			_logger.LogDebug("Printer path is available, trying to set up connection");
			_printer = new FilePrinter(filePath: _configuration.PrinterPath, createIfNotExists: false);
			logger.LogInformation("Set up printer connection");
		}
	}

	#endregion
	
	public Task PrintOrderTicketAsync(OrderTicketData data, CancellationToken cancellationToken = default)
	{
		_logger.LogDebug("Printing order ticket");
		
		if (!_configuration.PrinterAvailable) // TODO printer obj status
		{
			_logger.LogDebug("Printer not available, returning");
			return Task.CompletedTask;
		}

		var e = new EPSON();
		_printer.Write(
			e.PrintLine("ORDER TICKET"), 
			e.PrintLine(data.Id.ToSafeString()), 
			e.PartialCutAfterFeed(5)
		);
		_logger.LogDebug("Sent order ticket to printer");
		
		return Task.CompletedTask;
	}
}