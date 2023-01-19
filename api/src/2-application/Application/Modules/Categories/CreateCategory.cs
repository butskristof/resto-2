namespace Resto.Application.Modules.Categories;

public static class CreateCategory
{
	public class Request
	{
		public string Name { get; set; }
	}

	public class Response
	{
		public Guid Id { get; set; }
	}
	
	// validator
	
	// handler
	// name: required & unique (w/ lowercase)
}