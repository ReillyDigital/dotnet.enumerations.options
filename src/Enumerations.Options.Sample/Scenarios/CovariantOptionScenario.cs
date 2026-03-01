public static class CovariantOptionScenario
{
	public class Message(string text)
	{
		public string Text { get; protected set; } = text;
	}

	public sealed class RepeatedMessage(string text, int times)
		: Message(string.Join(' ', Enumerable.Repeat(text, times)))
	{
		public int Times { get; } = times;
	}

	public static IOption<Message> GetMessage(int? messageId, int repeatCount = 0)
	{
		if (messageId is null)
		{
			return BoxedError<Message>("Empty IDs are not supported.");
		}
		var message = messageId switch
		{
			1 => "Here is the message for 1.",
			2 => "Here is the message for 2.",
			3 => "Here is the message for 3.",
			_ => null
		};
		if (message is null)
		{
			return BoxedNone<Message>();
		}
		if (repeatCount > 1)
		{
			return BoxedSome(new RepeatedMessage(message, repeatCount));
		}
		return BoxedSome(new Message(message));
	}

	public static void Run()
	{
		switch (GetMessage(2, repeatCount: 3))
		{
			case IError error:
				throw new Exception(error.Value);
			case INone:
				Console.WriteLine("No message found.");
				break;
			case ISome<RepeatedMessage> some:
				var repeatedMessage = some.Value;
				Console.WriteLine(
					$"Message \"{repeatedMessage.Text}\" repeated {repeatedMessage.Times} times."
				);
				break;
			case ISome<Message> some:
				var message = some.Value;
				Console.WriteLine($"Message \"{message.Text}\" did not repeat.");
				break;
		}
	}
}
