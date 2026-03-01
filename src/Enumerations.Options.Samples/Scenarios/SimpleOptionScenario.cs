public static class SimpleOptionScenario
{
	public static Option<string> GetMessage(int? messageId)
	{
		if (messageId is null)
		{
			return OptionError<string>("Empty IDs are not supported.");
		}
		return messageId switch
		{
			1 => Some("Here is the message for 1."),
			2 => Some("Here is the message for 2."),
			3 => Some("Here is the message for 3."),
			_ => None<string>()
		};
	}

	public static void Run()
	{
		switch (GetMessage(2))
		{
			case { Type: OptionType.Error, ErrorValue: var error }:
				throw new Exception(error);
			case { Type: OptionType.None }:
				Console.WriteLine("No message found.");
				break;
			case { Type: OptionType.Some, Value: var some }:
				Console.WriteLine(some);
				break;
		}
	}
}
