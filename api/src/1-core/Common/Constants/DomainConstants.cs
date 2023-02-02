using System.Text.RegularExpressions;

namespace Resto.Common.Constants;

public static class DomainConstants
{
	public const int HexColorStringLength = 7;
	public static readonly Regex HexColorRegex = new(@"^#([A-F0-9]{6}|[A-F0-9]{3})$", RegexOptions.IgnoreCase);
}