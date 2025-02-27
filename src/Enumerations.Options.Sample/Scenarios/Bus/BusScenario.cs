public static class BusScenario
{
	public static void Run() => RunAsync().Wait();

	private static async Task RunAsync()
	{
		var provider = new BusProvider();
		var bus = provider.GetBus();
		var completion = new TaskCompletionSource();
		bus.SomeReceived +=
			(object? sender, ISome<string> some) => Console.WriteLine(some.Value);
		bus.ErrorReceived +=
			(object? sender, IError<string> error) =>
				Console.WriteLine(error.Value.Message);
		bus.EndReceived +=
			(object? sender, IEnd<string> error) => completion.SetResult();
		provider.DoStuff();
		await completion.Task;
	}

	private class BusProvider
	{
		private OptionBus<string> Bus { get; } = new();

		public void DoStuff()
		{
			Bus
				.Some("This is a bussed value.")
				.Some("This is another bussed value.")
				.Error("Oops. Bussed error.")
				.Some("One more bussed value.")
				.End();
		}

		public ReadOnlyOptionBus<string> GetBus() => Bus.AsReadOnly();
	}
}
