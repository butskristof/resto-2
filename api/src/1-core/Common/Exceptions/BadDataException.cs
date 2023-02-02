using Resto.Common.Enumerations;

namespace Resto.Common.Exceptions;

/// <summary>
/// This exception should be used when the user passed in invalid data in their request,
/// e.g. mismatched id's in the route and request body, or providing values for both properties
/// when only one should be given
/// </summary>
public class BadDataException : Exception
{
	public BadDataException(ErrorCode code, string propertyName)
	: base($"{propertyName}: {code}") {}
	
	public BadDataException(ErrorCode code)
	: base(code.ToString()) {}
	
	public BadDataException(string message)
	: base(message) {}
}