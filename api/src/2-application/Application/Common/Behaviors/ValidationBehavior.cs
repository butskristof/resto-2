using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ValidationException = Resto.Application.Common.Exceptions.ValidationException;

namespace Resto.Application.Common.Behaviors;

internal sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : IBaseRequest
{
	#region construction

	private readonly ILoggerFactory _loggerFactory;
	private readonly IEnumerable<IValidator<TRequest>> _validators;

	public ValidationBehavior(ILoggerFactory loggerFactory, IEnumerable<IValidator<TRequest>> validators)
	{
		_loggerFactory = loggerFactory;
		_validators = validators;
	}

	#endregion

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken _)
	{
		var logger = _loggerFactory.CreateLogger<TRequest>();
		var requestName = typeof(TRequest).Name;

		if (_validators.Any())
		{
			logger.LogDebug("Applying {Count} validators for request type {RequestName}",
				_validators.Count(), requestName);

			var context = new ValidationContext<TRequest>(request);
			var results = await Task.WhenAll(
				_validators.Select(v => v.ValidateAsync(context, _)));
			var failures = results
				.Where(e => e.Errors.Any())
				.SelectMany(e => e.Errors)
				.ToList();

			if (failures.Any())
			{
				logger.LogWarning("Validation failed, found {Count} errors", failures.Count);
				throw new ValidationException(failures);
			}
		}
		else
		{
			logger.LogDebug("No validators have been found for request type {RequestName}", requestName);
		}

		return await next();
	}
}