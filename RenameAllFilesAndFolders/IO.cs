public class IO
{
	public string Input(string? write = null)
	{
		Console.Write(write);
		return Console.ReadLine() ?? throw new Exception("Input was cancelled");
	}
	public Value InputValue(string start, string valueString, ConsoleColor consoleColor, string? write = null, string? end = null)
	{
		Console.Write(start);
		WriteColored(new() { Color = consoleColor, StringValue = valueString });
		var value2 = new Value() { Color = consoleColor, StringValue = Input(write) };
		Console.Write(end);
		return value2;
	}

	public void WriteColored(Value write)
	{
		Console.ForegroundColor = write.Color;
		Console.Write(write);
		Console.ResetColor();
	}
	public void WriteLineColored(Value write)
	{
		WriteColored(write);
		Console.WriteLine();
	}

	/// <summary>
	/// Writes <paramref name="entry"/> with first entry of <paramref name="firstValueThatBeColored"/> colored
	/// </summary>
	public void WriteFirstColorized(string entry, Value firstValueThatBeColored, int startIndex = 0)
	{
		var (left, right) = entry.TrimFirst(firstValueThatBeColored, startIndex);

		Console.Write(left);
		WriteColored(firstValueThatBeColored);
		Console.Write(right);
	}

	public Value WriteValue(string start, string valueString, ConsoleColor consoleColor, string? write = null, string? end = null)
	{
		Console.Write(start);
		var value = new Value() { Color = consoleColor, StringValue = valueString };
		WriteColored(value);
		Console.Write(write);
		Console.Write(end);
		return value;
	}
}

internal static class IOExtensions
{
	public static void WriteLineEntry(this IO io, FileSystemInfo entry, Value valueThatBeColored)
	{
		// Maybe never null
		string? entryDir = Path.GetDirectoryName(entry.FullName);

		Console.Write(entry.Attributes is FileAttributes.Directory ? "[ DIR] " : "[FILE] ");
		io.WriteFirstColorized(entry.FullName, valueThatBeColored, startIndex: entryDir.Length);
		Console.WriteLine();
	}

	public static Value InputValue(this IO io, ConsoleColor consoleColor, string? write = null, string? end = null)
		=> io.InputValue("Input ", "value", consoleColor, write, end);

	/// <summary>
	/// If <paramref name="end"/> is <see langword="null"/> it will be <see cref="Environment.NewLine"/>
	/// </summary>
	public static Value FoundValue(this IO io, string whatFound, ConsoleColor consoleColor, string? write = null, string? end = null)
		=> io.WriteValue("Found ", whatFound, consoleColor, write, end ?? Environment.NewLine);
}