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
		
		if (_configuration.UsePrinter)
		{
			_logger.LogDebug("Printer path is available, trying to set up connection");
			if (File.Exists(_configuration.PrinterPath))
			{
				_printer = new FilePrinter(filePath: _configuration.PrinterPath, createIfNotExists: false);
				
				_logger.LogInformation("Set up printer connection");
			}
			else
			{
				_logger.LogWarning("Printer path is available, but file does not exist");
			}
		}
	}

	#endregion
	
	public Task PrintOrderTicketAsync(OrderTicketData data, CancellationToken cancellationToken = default)
	{
		_logger.LogDebug("Printing order ticket");

		if (_printer == null)
		{
			_logger.LogDebug("Printer not available");
			return Task.CompletedTask;
		}

		var content = GetOrderTicketContent(data);
		_printer.Write(content);
		_logger.LogDebug("Sent order ticket to printer");
		
		return Task.CompletedTask;
	}

	private const int CurrencyWidth = 10;
	private const int QuantityWidth = 4;
	private const int TabWidth = 4;

		private string FormatCurrency(decimal value)
		=> value.ToString("N2");
	
	private byte[][] GetOrderTicketContent(OrderTicketData data)
	{
		var e = new EPSON();
		var content = new List<byte[]>();
		
		content.Add(e.CenterAlign());

		if (!string.IsNullOrWhiteSpace(_configuration.HeaderImagePath) && File.Exists(_configuration.HeaderImagePath))
		{
			content.Add(e.PrintImage(File.ReadAllBytes(_configuration.HeaderImagePath), true, false, 550));
		}
		else
		{
			content.Add(e.PrintLine("KLJ Wiekevorst"));
			content.Add(e.PrintLine("Restaurantdag 2023"));
		}
		
		content.Add(e.LeftAlign());
		content.Add(e.FeedLines(1));
		content.Add(e.PrintLine("Tafel:"));
		content.Add(e.FeedLines(4));
		
		content.Add(e.PrintLine("================================================"));
		content.Add(e.PrintLine($"Order ID: {data.Id.ToSafeString()}"));
		content.Add(e.PrintLine($"Timestamp: {data.Timestamp:u}"));
		content.Add(e.PrintLine("================================================"));
		content.Add(e.FeedLines(1));
		
		foreach (var orderLine in data.OrderLines)
		{
			content.Add(e.LeftAlign());
			content.Add(e.PrintLine($"{orderLine.Quantity,-QuantityWidth}{orderLine.Product.Name}"));
			foreach (var orderLineTopping in orderLine.Toppings)
			{
				content.Add(e.PrintLine(
					$"{string.Empty,-QuantityWidth}{string.Empty,-TabWidth}{orderLineTopping.Name}"
					));
			}
			content.Add(e.RightAlign());
			content.Add(e.PrintLine(
				$"{FormatCurrency(orderLine.Price),+CurrencyWidth}{FormatCurrency(orderLine.OrderLineTotal),+CurrencyWidth}"
				));
		}
		
		content.Add(e.LeftAlign());
		content.Add(e.PrintLine("------------------------------------------------"));
		content.Add(e.RightAlign());
		content.Add(e.PrintLine($"Totaal:{FormatCurrency(data.OrderTotal),+CurrencyWidth}"));
		content.Add(e.LeftAlign());
	
		content.Add(e.FeedLines(3));
		content.Add(e.PartialCutAfterFeed(10));
		
		return content.ToArray();
	}
}