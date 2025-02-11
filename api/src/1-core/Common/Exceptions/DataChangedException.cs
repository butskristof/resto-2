using Resto.Common.Enumerations;

namespace Resto.Common.Exceptions;

/// <summary>
/// This exception should be used when a user tries to update data which has a newer
/// concurrency timestamp (resulting in a conflict http response)
/// </summary>
public sealed class DataChangedException : Exception
{
	public DataChangedException()
	: base(ErrorCode.DataChanged.ToString())
	{
	}
}