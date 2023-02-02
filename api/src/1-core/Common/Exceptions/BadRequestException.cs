using Resto.Common.Enumerations;

namespace Resto.Common.Exceptions;

/// <summary>
/// This exception should be used when the user requests an action that is not allowed
/// altogether, contrary to a Forbidden response where *they* are not allowed to do something
/// </summary>
public class BadRequestException : Exception
{
	public BadRequestException(string message)
		: base(message)
	{
	}

	public BadRequestException(ErrorCode errorCode) : base(errorCode.ToString())
	{
	}
}