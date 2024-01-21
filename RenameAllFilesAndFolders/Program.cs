while (true)
{
	var io = new IO();

	// Can inject path provider (for example FromConfigFileProvider)
	string path = io.Input("Enter path(just 'enter' to current directory): ");

	if (string.IsNullOrWhiteSpace(path))
		path = AppContext.BaseDirectory;
	var directoryInfo = new DirectoryInfo(path);
	if(!directoryInfo.Exists)
	{
		Console.WriteLine("Path not exists, try again");
		continue;
	}

	io.WriteValue("Path: ", path, ConsoleColor.White, end:Environment.NewLine);

	// Can add settings for SearchOption
	// Maybe can add search pattern as input
	int countAll = Directory.GetFileSystemEntries(path, "*", SearchOption.AllDirectories).Length;
	io.FoundValue(countAll.ToString(), ConsoleColor.White, " files and directories");
	//Console.WriteLine($"Found {countAll} files and directories");
	if (countAll is 0)
		continue;

	inputValueToBeReplaced:
	Value valueThatBeReplaced = io.InputValue(ConsoleColor.Yellow, write:" that contains in file/directory names: ");

	if(valueThatBeReplaced.IsEmpty())
		goto inputValueToBeReplaced;

	// Can add settings what ignore case
	FileSystemInfo[] entriesFound = directoryInfo.GetFileSystemInfos($"*{valueThatBeReplaced}*", SearchOption.AllDirectories);
	io.FoundValue(entriesFound.Length.ToString(), ConsoleColor.White, write:" files and directories with containing ", end:string.Empty);
	io.WriteLineColored(valueThatBeReplaced);

	if (entriesFound.Length is 0)
		goto inputValueToBeReplaced;

	foreach (FileSystemInfo entry in entriesFound)
	{
		io.WriteLineEntry(entry, valueThatBeColored:valueThatBeReplaced);
	}

	Value value = io.InputValue(ConsoleColor.Blue, write: " to that be replaced: ");

	replaceQuestion:
	Console.Write("Replace all '");
	io.WriteColored(valueThatBeReplaced);
	Console.Write("' with '");
	io.WriteColored(value);
	string replaceAll = io.Input("'? y/n [y]:");

	if (replaceAll is "n")
		continue;
	if (replaceAll is not "y" && !string.IsNullOrWhiteSpace(replaceAll))
		goto replaceQuestion;

	int renamedCount = 0;
	// Tested only on windows 10
	foreach (var entry in entriesFound.Reverse())
	{
		entry.Rename(valueThatBeReplaced, value);

		renamedCount++;
	}

	Console.WriteLine($"Success renamed: {renamedCount}");
}