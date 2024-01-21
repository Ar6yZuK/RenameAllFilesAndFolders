public static class FileSystemInfoExtensions
{
	public static void Rename(this FileSystemInfo entry, string valueThatBeReplaced, string newValue)
	{
		var (left, right) = entry.Name.TrimFirst(valueThatBeReplaced);
		string newDirName = left + newValue + right;

		string entryFullName = entry.FullName;
		string entryDir = Path.GetDirectoryName(entryFullName)!;
		string newPath = Path.Combine(entryDir, newDirName);

		// Can move files and directories
		Directory.Move(entryFullName, newPath);
	}
}