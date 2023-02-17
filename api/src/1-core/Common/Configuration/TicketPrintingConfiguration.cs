namespace Resto.Common.Configuration;

public interface ITicketPrintingConfiguration
{
	string PrinterPath { get; }
	bool UsePrinter { get; }
}

public class TicketPrintingConfiguration : ITicketPrintingConfiguration
{
	public string PrinterPath { get; set; }
	public bool UsePrinter => !string.IsNullOrWhiteSpace(PrinterPath);
}