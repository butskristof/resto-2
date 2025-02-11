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
	private readonly FileStreamPrinter _printer;
	private readonly byte[] _headerImage;

	public TicketPrintingService(ILogger<TicketPrintingService> logger, ITicketPrintingConfiguration configuration)
	{
		_logger = logger;
		_configuration = configuration;
		
		if (_configuration.UsePrinter)
		{
			_logger.LogDebug("Printer path is available, trying to set up connection");
			if (File.Exists(_configuration.PrinterPath))
			{
				_printer = new FileStreamPrinter(filePath: _configuration.PrinterPath, createIfNotExists: false);
				
				_logger.LogInformation("Set up printer connection");
			}
			else
			{
				_logger.LogWarning("Printer path is available, but file does not exist");
			}
		}

		if (configuration.UseHeaderImage)
		{
			logger.LogDebug("Header image path is available, trying to read content");
			if (File.Exists(_configuration.HeaderImagePath))
			{
				_headerImage = File.ReadAllBytes(configuration.HeaderImagePath);
				logger.LogInformation("Set up ticket header image");
			}
			else
			{
				logger.LogWarning("Header image path is available, but file does not exist");
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

		var orderLines = data.OrderLines.Select(ol => ol).ToList();
		var soupOrderLines = data.OrderLines
			.Where(ol => ol.Product.Name.Contains("soep", StringComparison.InvariantCultureIgnoreCase))
			.ToList();

		var e = new EPSON();
		var ticketCommands = new List<byte[]>();
		
		if (soupOrderLines.Any())
		{
			orderLines.RemoveAll(ol => soupOrderLines.Contains(ol));
			
			SetOrderTicketHeader(ticketCommands, e, data);
			SetOrderTicketOrderLines(ticketCommands, e, soupOrderLines);
			SetTicketCut(ticketCommands, e);
		}
		
		SetOrderTicketHeader(ticketCommands, e, data);
		SetOrderTicketOrderLines(ticketCommands, e, orderLines);
		SetOrderTicketFooter(ticketCommands, e, data);
		SetTicketCut(ticketCommands, e);
		
		_printer.Write(ticketCommands.ToArray());
		_logger.LogDebug("Sent order ticket to printer");
		
		return Task.CompletedTask;
	}

	private const int CurrencyWidth = 10;
	private const int QuantityWidth = 4;
	private const int TabWidth = 4;

	private string FormatCurrency(decimal value)
		=> value.ToString("N2");

	private string GetDiscountPrintValue(OrderTicketData.OrderTicketDiscount discount)
	{
		return discount switch
		{
			OrderTicketData.OrderTicketDiscount.Member => "Leiding",
			OrderTicketData.OrderTicketDiscount.Volunteer => "Helper",
			_ => string.Empty,
		};
	}
	
	private void SetOrderTicketHeader(ICollection<byte[]> content, ICommandEmitter e, OrderTicketData data)
	{
		content.Add(e.CenterAlign());

		if (_headerImage != null)
		{
			content.Add(e.PrintImage(_headerImage, true, false, 550));
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
	}

	private void SetOrderTicketOrderLines(ICollection<byte[]> content, ICommandEmitter e,
		IEnumerable<OrderTicketData.OrderTicketOrderLine> orderLines)
	{
		foreach (var orderLine in orderLines)
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
			
			content.Add(e.LeftAlign());
		}
	}

	private void SetOrderTicketFooter(ICollection<byte[]> content, ICommandEmitter e, OrderTicketData data)
	{
		content.Add(e.PrintLine("------------------------------------------------"));
		
		if (data.Discount is not OrderTicketData.OrderTicketDiscount.None)
		{
			content.Add(e.PrintLine($"Korting: {GetDiscountPrintValue(data.Discount)}"));
		}
		
		content.Add(e.RightAlign());
		content.Add(e.PrintLine($"Totaal:{FormatCurrency(data.OrderTotal),+CurrencyWidth}"));
		content.Add(e.LeftAlign());
	}

	private void SetTicketCut(ICollection<byte[]> content, ICommandEmitter e)
	{
		content.Add(e.FeedLines(3));
		content.Add(e.PartialCutAfterFeed(10));
	}
}