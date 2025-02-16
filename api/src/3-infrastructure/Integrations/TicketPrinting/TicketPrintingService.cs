using ESCPOS_NET;
using ESCPOS_NET.Emitters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Resto.Common.Configuration;
using Resto.Common.Extensions;
using Resto.Common.Integrations.TicketPrinting;
using Resto.Common.Integrations.TicketPrinting.Models;

namespace Resto.Infrastructure.Integrations.TicketPrinting;

internal sealed class TicketPrintingService : ITicketPrintingService
{
	#region construction

	private readonly ILogger<TicketPrintingService> _logger;
	private readonly TicketPrintingSettings _settings;
	private readonly byte[] _headerImage;
	private readonly TimeProvider _timeProvider;

	private readonly FilePrinter _printer;

	public TicketPrintingService(ILogger<TicketPrintingService> logger, IOptions<TicketPrintingSettings> settings,
		TimeProvider timeProvider)
	{
		_logger = logger;
		_settings = settings.Value;
		_timeProvider = timeProvider;

		if (_settings.UsePrinter)
		{
			logger.LogInformation("Printer path is configured");
			if (File.Exists(_settings.PrinterPath))
			{
				logger.LogDebug("Trying to create printer instance");
				_printer = new FilePrinter(_settings.PrinterPath, false);
				logger.LogInformation("Created printer object");
			}
			else
			{
				logger.LogWarning("Printer path is configured but file does not exist");
			}
		}
		else
		{
			logger.LogInformation("Printer path is not configured");
		}

		if (_settings.UseHeaderImage)
		{
			logger.LogDebug("Header image path is available, trying to read content");
			if (File.Exists(_settings.HeaderImagePath))
			{
				_headerImage = File.ReadAllBytes(_settings.HeaderImagePath);
				logger.LogInformation("Set up ticket header image");
			}
			else
			{
				logger.LogWarning("Header image path is available, but file does not exist");
			}
		}
	}

	#endregion
	
	public async Task PrintOrderTicketAsync(OrderTicketData data, CancellationToken cancellationToken = default)
	{
		_logger.LogDebug("Printing order ticket");

		if (_printer is null)
		{
			_logger.LogDebug("Printer not available");
			return;
		}

		var orderLines = data.OrderLines.Select(ol => ol).ToList();
		var soupOrderLines = data.OrderLines
			.Where(ol => ol.Product.Name.Contains("soep", StringComparison.InvariantCultureIgnoreCase))
			.ToList();

		var e = new EPSON();
		var ticketCommands = new List<byte[]>();
		
		if (soupOrderLines.Count > 0)
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

		// wait a bit before returning which will dispose the printer object
		// this should give the printer time to clear it's pass its buffer
		// content to the BinaryWriter
		// TODO verify what time we can get away with
		await Task.Delay(100, CancellationToken.None);
	}

	private const int CurrencyWidth = 10;
	private const int QuantityWidth = 4;
	private const int TabWidth = 4;

	private static string FormatCurrency(decimal value) => value.ToString("N2");

	private static string GetDiscountPrintValue(OrderTicketData.OrderTicketDiscount discount)
        => discount switch
		{
			OrderTicketData.OrderTicketDiscount.Member => "Leiding",
			OrderTicketData.OrderTicketDiscount.Volunteer => "Helper",
			_ => string.Empty,
		};
	
	private void SetOrderTicketHeader(List<byte[]> content, EPSON e, OrderTicketData data)
	{
		content.Add(e.CenterAlign());

		if (_headerImage != null)
		{
			content.Add(e.PrintImage(_headerImage, true, false, 550));
		}
		else
		{
			content.Add(e.PrintLine("KLJ Wiekevorst"));
			content.Add(e.PrintLine($"Restaurantdag {_timeProvider.GetLocalNow().Year}"));
		}
		
		content.Add(e.LeftAlign());
		content.Add(e.FeedLines(1));
		content.Add(e.PrintLine("Tafel:"));
		content.Add(e.FeedLines(4));
		
		content.Add(e.PrintLine("================================================"));
		content.Add(e.PrintLine($"Order ID: {data.Id.ToSafeString()}"));
		content.Add(e.PrintLine($"Timestamp: {data.Timestamp.LocalDateTime:dd/MM/yyyy HH:mm:ss}"));
		content.Add(e.PrintLine("================================================"));
		content.Add(e.FeedLines(1));
	}

	private static void SetOrderTicketOrderLines(List<byte[]> content, EPSON e,
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

	private static void SetOrderTicketFooter(List<byte[]> content, EPSON e, OrderTicketData data)
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

	private static void SetTicketCut(List<byte[]> content, EPSON e)
	{
		content.Add(e.FeedLines(3));
		content.Add(e.PartialCutAfterFeed(10));
	}
}