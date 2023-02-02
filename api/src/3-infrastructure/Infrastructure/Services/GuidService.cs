using Resto.Common.Services;

namespace Resto.Infrastructure.Services;

internal class GuidService : IGuid
{
	public Guid NewGuid() => Guid.NewGuid();
}