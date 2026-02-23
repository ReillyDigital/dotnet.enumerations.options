public static class BusScenario
{
	public static void Run() => RunAsync().Wait();

	private static async Task RunAsync()
	{
		var provider = new BusProvider();
		var bus = provider.GetBus();
		var completion = new TaskCompletionSource();
		bus.SomeReceived += (sender, option) => Console.WriteLine(option.Value);
		bus.ErrorReceived += (sender, option) => Console.WriteLine(option.ErrorValue);
		bus.EndReceived += (sender, e) => completion.SetResult();
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
