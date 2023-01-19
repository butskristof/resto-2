using System.ComponentModel;
using FluentValidation;
using Resto.Application.Common.Extensions;
using Resto.Common.Constants;
using Resto.Common.Enumerations;

namespace Resto.Application.Common.Contracts.Requests.Common;

public abstract class PagedRequest
{
	[DefaultValue(ApplicationConstants.DefaultPage)]
	public int Page { get; set; } = ApplicationConstants.DefaultPage;

	[DefaultValue(ApplicationConstants.DefaultPageSize)]
	public int PageSize { get; set; } = ApplicationConstants.DefaultPageSize;
}

public abstract class PagedRequestValidator<T> : AbstractValidator<T>
	where T : PagedRequest
{
	protected PagedRequestValidator()
	{
		RuleFor(e => e.Page)
			.GreaterThanOrEqualTo(1)
			.WithErrorCode(ErrorCode.Invalid);
		RuleFor(e => e.PageSize)
			.GreaterThanOrEqualTo(1)
			.WithErrorCode(ErrorCode.Invalid)
			.LessThanOrEqualTo(ApplicationConstants.MaxPageSize)
			.WithErrorCode(ErrorCode.Invalid);
	}
}