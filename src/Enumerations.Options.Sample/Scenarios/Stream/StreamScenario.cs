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
		private OptionStream<string> Stream { get; } = new(bufferSize: 2);

		public async Task DoStuff()
		{
			await Stream.Some("This is a streamed value.");
			await Stream.Some("This is another streamed value.");
			await Stream.Error("Oops. Streamed error.");
			await Stream.Some("One more streamed value.");
			await Stream.End();
		}

		public ReadOnlyOptionStream<string> GetStream() => Stream.AsReadOnly();
	}
}
