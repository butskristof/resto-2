using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Resto.Application.Common.Behaviors;

internal sealed class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : IBaseRequest
{
	#region construction

	private readonly ILoggerFactory _loggerFactory;

	public LoggingBehavior(ILoggerFactory loggerFactory)
	{
		_loggerFactory = loggerFactory;
	}

	#endregion

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		var logger = _loggerFactory.CreateLogger<TRequest>();
		
		var requestName = typeof(TRequest).Name;
		logger.LogDebug("Incoming request: {RequestName}", requestName);

		TResponse response;
		var stopwatch = Stopwatch.StartNew();
		
		try
		{
			response = await next();
		}
		finally
		{
			stopwatch.Stop();

			logger.LogDebug("Completed request {RequestName} in {ElapsedMilliseconds} ms",
				requestName, stopwatch.ElapsedMilliseconds);
		}

		return response;
	}
}