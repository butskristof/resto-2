using FluentValidation;
using Resto.Common.Enumerations;

namespace Resto.Application.Common.Extensions;

internal static class FluentValidationExtensions
{
	public static IRuleBuilderOptions<T, TProperty> WithErrorCode<T, TProperty>(
		this IRuleBuilderOptions<T, TProperty> rule,
		ErrorCode errorCode)
	{
		return rule.WithMessage(errorCode.ToString());
	}
}