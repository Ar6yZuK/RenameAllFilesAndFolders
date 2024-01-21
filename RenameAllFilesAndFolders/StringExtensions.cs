public static class StringExtensions
{
	public static (string left, string right) TrimFirst(this string text, string valueToTrim, int startIndex = 0)
	{
		int index = text.IndexOf(valueToTrim, startIndex);

		return (text[..index],
				text[(index + valueToTrim.Length)..]);
	}
}