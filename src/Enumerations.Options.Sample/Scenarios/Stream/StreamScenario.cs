public static class StreamScenario
{
	public static void Run() => RunAsync().Wait();

	private static async Task RunAsync()
	{
		var provider = new StreamProvider();
		var stream = provider.GetStream();
		await Task.WhenAll(
			Task.Run(async () =>
			{
				await foreach (var each in stream.ReadToEnd())
				{
					switch (each)
					{
						case IError error:
							Console.WriteLine(error.Value.Message);
							break;
						case ISome<string> some:
							Console.WriteLine(some.Value);
							break;
					}
				}
			}),
			Task.Run(provider.DoStuff)
		);
	}

	private class StreamProvider
	{
		private OptionStream<string> Stream { get; } = new();

		public void DoStuff()
		{
			Stream
				.Some("This is a streamed value.")
				.Some("This is another streamed value.")
				.Error("Oops. Streamed error.")
				.Some("One more streamed value.")
				.End();
		}

		public ReadOnlyOptionStream<string> GetStream() => Stream.AsReadOnly();
	}
}
