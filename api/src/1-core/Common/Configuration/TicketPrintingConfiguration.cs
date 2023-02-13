namespace Resto.Common.Configuration;

public interface ITicketPrintingConfiguration
{
	string PrinterPath { get; }
	bool PrinterAvailable { get; }
}

public class TicketPrintingConfiguration : ITicketPrintingConfiguration
{
	public string PrinterPath { get; set; }
	public bool PrinterAvailable => !string.IsNullOrWhiteSpace(PrinterPath);
	// TODO verify file exists
}