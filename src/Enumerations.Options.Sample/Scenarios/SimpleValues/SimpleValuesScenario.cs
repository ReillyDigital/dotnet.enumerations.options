public static class SimpleValuesScenario
{
	public static IOption<string> GetMessage(int? messageId)
	{
		if (messageId is null)
		{
			return Error<string>(new NotSupportedException("Empty IDs are not supported."));
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
			case IError error:
				throw error.Value;
			case INone:
				Console.WriteLine("No message found");
				break;
			case ISome<string> some:
				Console.WriteLine(some.Value);
				break;
		}
	}
}
