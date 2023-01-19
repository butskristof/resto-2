using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Resto.Application.Common.Behaviors;

namespace Resto.Application;

public static class DependencyInjectionExtensions
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddAutoMapper(Assembly.GetExecutingAssembly());
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
		
		services.AddMediatR(Assembly.GetExecutingAssembly());
		services.AddMediatrPipelineBehaviors();
		
		return services;
	}

	private static IServiceCollection AddMediatrPipelineBehaviors(this IServiceCollection services)
	{
		// order matters!
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
		
		return services;
	}
}