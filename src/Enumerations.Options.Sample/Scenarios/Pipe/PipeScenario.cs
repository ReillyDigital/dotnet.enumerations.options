public static class PipeScenario
{
	public static void Run() => RunAsync().Wait();

	private static async Task RunAsync()
	{
		var provider = new PipeProvider();
		var pipe = provider.GetPipe();
		pipe.SomeReceived +=
			(object? sender, ISome<string> some) => Console.WriteLine(some.Value);
		pipe.ErrorReceived +=
			(object? sender, IError<string> error) =>
				Console.WriteLine(error.Value.Message);
		provider.DoStuff();
		await pipe.Ended;
	}

	private class PipeProvider
	{
		private OptionPipe<string> Pipe { get; } = new();

		public void DoStuff()
		{
			Pipe
				.Some("This is a piped value.")
				.Some("This is another piped value.")
				.Error("Oops. Piped error.")
				.Some("One more piped value.")
				.End();
		}

		public ReadOnlyOptionPipe<string> GetPipe() => Pipe.AsReadOnly();
	}
}
