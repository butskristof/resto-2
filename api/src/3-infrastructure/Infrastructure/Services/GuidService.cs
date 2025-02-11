using Resto.Common.Services;

namespace Resto.Infrastructure.Services;

internal sealed class GuidService : IGuid
{
	public Guid NewGuid() => Guid.NewGuid();
}