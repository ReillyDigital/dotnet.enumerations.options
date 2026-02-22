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
					switch (each.Type)
					{
						case OptionType.Error:
							Console.WriteLine(each.ErrorValue.Message);
							break;
						case OptionType.Some:
							Console.WriteLine(each.Value);
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
			Stream.End();
		}

		public ReadOnlyOptionStream<string> GetStream() => Stream.AsReadOnly();
	}
}
