using Resto.Common.Integrations.TicketPrinting.Models;

namespace Resto.Common.Integrations.TicketPrinting;

public interface ITicketPrintingService
{
	Task PrintOrderTicketAsync(OrderTicketData data, CancellationToken cancellationToken = default);
}