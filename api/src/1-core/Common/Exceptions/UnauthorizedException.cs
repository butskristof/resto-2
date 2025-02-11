using Resto.Common.Enumerations;

namespace Resto.Common.Exceptions;

public sealed class UnauthorizedException : Exception
{
	public UnauthorizedException(ErrorCode code) : base(code.ToString())
	{
	}
}