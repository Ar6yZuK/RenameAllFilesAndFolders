public class Value
{
	public bool IsEmpty()
		=> string.IsNullOrWhiteSpace(StringValue);

	public required ConsoleColor Color { get; init; }
	public required string StringValue { get; set; }

	public static implicit operator string(Value item)
		=> item.StringValue;
	/// <summary>
	/// override ToString for Console.WriteLine(object)
	/// </summary>
	public override string ToString()
		=> StringValue;
}