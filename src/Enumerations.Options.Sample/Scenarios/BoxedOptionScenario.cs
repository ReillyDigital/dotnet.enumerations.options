public static class BoxedOptionScenario
{
	public static IOption<string> GetMessage(int? messageId)
	{
		if (messageId is null)
		{
			return BoxedError<string>("Empty IDs are not supported.");
		}
		return messageId switch
		{
			1 => BoxedSome("Here is the message for 1."),
			2 => BoxedSome("Here is the message for 2."),
			3 => BoxedSome("Here is the message for 3."),
			_ => BoxedNone<string>()
		};
	}

	public static void Run()
	{
		switch (GetMessage(2))
		{
			case IError error:
				throw new Exception(error.Value);
			case INone:
				Console.WriteLine("No message found.");
				break;
			case ISome<string> some:
				Console.WriteLine(some.Value);
				break;
		}
	}
}
