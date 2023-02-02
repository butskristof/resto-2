using Resto.Common.Services;

namespace Resto.Infrastructure.Services;

internal class DateTimeService : IDateTime
{
	public DateTime Now => DateTime.Now;
}