namespace Resto.Common.Configuration;

public sealed class TicketPrintingSettings
{
	public const string SectionName = "TicketPrinting";
	
	public string PrinterPath { get; set; }
	public bool UsePrinter => !string.IsNullOrWhiteSpace(PrinterPath);

	public string HeaderImagePath { get; set; }
	public bool UseHeaderImage => !string.IsNullOrWhiteSpace(HeaderImagePath);
}