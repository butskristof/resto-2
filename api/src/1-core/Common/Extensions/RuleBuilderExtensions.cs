using FluentValidation;
using Resto.Common.Constants;

namespace Resto.Common.Extensions
{
	public static class RuleBuilderExtensions
	{
		public static IRuleBuilderOptions<T, string> HexColor<T>(this IRuleBuilderOptions<T, string> ruleBuilder)
		{
			var options = ruleBuilder
				.Matches(DomainConstants.HexColorRegex);
			return options;
		}
	}
}