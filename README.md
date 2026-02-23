# ReillyDigital.Enumerations.Enumeration.Options

A result object library for .NET.

## Usage

### Simple Values

Define a function that returns an `Option<>` value:
```csharp
static Option<string> GetMessage(int? messageId)
{
	if (messageId is null)
	{
		return OptionError<string>("Empty IDs are not supported.");
	}
	return messageId switch
	{
		1 => Some("Here is the message for 1."),
		2 => Some("Here is the message for 2."),
		3 => Some("Here is the message for 3."),
		_ => None<string>()
	};
}
```

Check the result value against the various option types:
```csharp
	switch (GetMessage(2))
	{
		case { Type: OptionType.Error, ErrorValue: var error }:
			throw new Exception(error);
	case { Type: OptionType.None }:
		Console.WriteLine("No message found");
		break;
	case { Type: OptionType.Some, Value: var value }:
		Console.WriteLine(value);
		break;
}
```

### Value Streams

Define a class that provides an option stream.
```csharp
class StreamProvider
{
	private OptionStream<string> Stream { get; } = new();

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
```

Get the provided stream:
```csharp
var provider = new StreamProvider();
var stream = provider.GetStream();
```

Tell the stream provider to do stuff:
```csharp
provider.DoStuff();
```

Iterate over the stream options until an end of stream is provided:
```csharp
await foreach (var each in stream.ReadToEnd())
{
	switch (each.Type)
	{
		case OptionType.Error:
			Console.WriteLine(each.ErrorValue);
			break;
		case OptionType.Some:
			Console.WriteLine(each.Value);
			break;
	}
}
```

### Value Bus

Define a class that provides an option bus.
```csharp
class BusProvider
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
```

Get the provided bus:
```csharp
var provider = new BusProvider();
var bus = provider.GetBus();
```

Add handlers to the bus for the various option types:
```csharp
var completion = new TaskCompletionSource();
bus.SomeReceived += (sender, option) => Console.WriteLine(option.Value);
bus.ErrorReceived += (sender, option) =>
	Console.WriteLine(option.ErrorValue);
bus.EndReceived += (sender, e) => completion.SetResult();
```

Tell the bus provider to do stuff:
```csharp
provider.DoStuff();
```

Await the end of the bus:
```csharp
await completion.Task;
```

## Links

Sample Project:
https://gitlab.com/reilly-digital/dotnet/enumerations.options/-/tree/main/src/Enumerations.Options.Sample

NuGet:
https://www.nuget.org/packages/ReillyDigital.Enumerations.Options
