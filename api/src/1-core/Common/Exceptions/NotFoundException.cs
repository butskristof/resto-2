﻿namespace Resto.Common.Exceptions;

/// <summary>
/// This exception should be used when the requested data is not present
/// </summary>
public sealed class NotFoundException : Exception
{
	public NotFoundException(string message)
		: base(message)
	{
	}
}