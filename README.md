# ReillyDigital.Enumerations.Options

A lightweight, zero-allocation result object library for .NET with discriminated union types for void, value, none, and error outcomes.

## Features

- **Option Types** - `Option<TValue>` represents values that can be `Some`, `None`, or `Error`.
- **Void Types** - `Void` for operations without return values that can still fail.
- **Zero Allocation** - Struct-based types avoid heap allocations in hot paths.
- **Boxed Interfaces** - `IVoid and IOption<TValue>` for polymorphism and covariance when needed, while providing minimal allocation boxing.
- **Pattern Matching** - Full support for C# switch expressions and pattern matching.
- **Enumerables** - `IEnumerable` and `IAsyncEnumerable` implementations for option collections.
- **Streaming** - `OptionStream<T>` for async streaming of option values.
- **Event Bus** - `OptionBus<T>` for event-based option distribution.

## Usage

### Simple Option Values

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
		Console.WriteLine("No message found.");
		break;
	case { Type: OptionType.Some, Value: var value }:
		Console.WriteLine(value);
		break;
}
```

### Boxed Option Values

Define a function that returns an `IOption<>` value:
```csharp
static IOption<string> GetMessage(int? messageId)
{
	if (messageId is null)
	{
		return BoxedError<string>("Empty IDs are not supported.");
	}
	return messageId switch
	{
		1 => BoxedSome("Here is the message for 1."),
		2 => BoxedSome("Here is the message for 2."),
		3 => BoxedSome("Here is the message for 3."),
		_ => BoxedNone<string>()
	};
}
```

Check the result value against the various boxed option types:
```csharp
switch (GetMessage(2))
{
	case IError error:
		throw new Exception(error.Value);
	case INone:
		Console.WriteLine("No message found.");
		break;
	case ISome<string> some:
		Console.WriteLine(some.Value);
		break;
}
```

### Covariant Option Values

Boxed options support covariance through their interfaces. Define types for the option value:
```csharp
class Message(string text)
{
	public string Text { get; protected set; } = text;
}

sealed class RepeatedMessage(string text, int times)
	: Message(string.Join(' ', Enumerable.Repeat(text, times)))
{
	public int Times { get; } = times;
}
```

Define a function that returns different derived types:
```csharp
static IOption<Message> GetMessage(int? messageId, int repeatCount = 0)
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
```

Check the result value against specific derived types:
```csharp
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
	switch (each)
	{
		case { Type: OptionType.Error, ErrorValue: var error }:
			Console.WriteLine(error);
			break;
		case { Type: OptionType.Some, Value: var value }:
			Console.WriteLine(value);
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
