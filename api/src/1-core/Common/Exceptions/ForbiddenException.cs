using Resto.Common.Enumerations;

namespace Resto.Common.Exceptions;

/// <summary>
/// This exception should be used when a user requests an action they don't have
/// the right permission for
/// </summary>
public sealed class ForbiddenException : Exception
{
	public ForbiddenException() : base() {}
	
	public ForbiddenException(string message)
		: base(message)
	{
	}

	public ForbiddenException(ErrorCode code)
		: base(code.ToString())
	{
	}
}