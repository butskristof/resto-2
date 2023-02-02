namespace Resto.Application.Common.Exceptions;

public class MappingException : Exception
{
	public MappingException() : base("Could not map entity") {}
}