public static class StreamScenario
{
	public static void Run() => RunAsync().Wait();

	private static async Task RunAsync()
	{
		var provider = new StreamProvider();
		var stream = provider.GetStream();
		stream.SomeReceived +=
			(object? sender, Some<string> some) => Console.WriteLine(some.Value);
		stream.ErrorReceived +=
			(object? sender, Error<string> error) =>
				Console.WriteLine("I should log this error.");
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
