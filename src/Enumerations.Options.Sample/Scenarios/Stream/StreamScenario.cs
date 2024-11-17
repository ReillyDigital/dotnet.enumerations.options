public static class StreamScenario
{
	public static void Run() => RunAsync().Wait();

	private static async Task RunAsync()
	{
		var provider = new StreamProvider();
		var stream = provider.GetStream();
		stream.SomeReceived +=
			(object? sender, IOption<string> option) =>
				Console.WriteLine(option.Value);
		stream.ErrorReceived +=
			(object? sender, IOption<string> option) =>
				Console.WriteLine(((IError)option).Value.Message);
		provider.DoStuff();
		await stream.EndOfStream;
	}

	private class StreamProvider
	{
		private OptionStream<string> Stream { get; } = new();

		public void DoStuff()
		{
			Stream
				.Some("This is a value.")
				.Some("This is another value.")
				.Error("Oops.")
				.Some("One more value.")
				.End();
		}

		public ReadOnlyOptionStream<string> GetStream() => Stream.AsReadOnly();
	}
}
