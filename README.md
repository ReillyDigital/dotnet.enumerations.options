# ReillyDigital.Enumerations.Enumeration.Options

A result object library for .NET.

## Usage

### Simple Values

Define a function that returns an `IOption<>` value:
```csharp
static IOption<string> GetMessage(int? messageId)
{
	if (messageId is null)
	{
		return Error<string>(
			new NotSupportedException("Empty IDs are not supported.")
		);
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
	case IError error:
		throw error.Value;
	case INone:
		Console.WriteLine("No message found");
		break;
	case ISome<string> some:
		Console.WriteLine(some.Value);
		break;
}
```

### Value Streams

Define a class that provides an option stream.
```csharp
class StreamProvider
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
```

Get the provided stream:
```csharp
var provider = new StreamProvider();
var stream = provider.GetStream();
```

Add handlers to the stream for the various option types:
```csharp
stream.SomeReceived +=
	(object? sender, IOption<string> option) =>
		Console.WriteLine(option.Value);
stream.ErrorReceived +=
	(object? sender, IOption<string> option) =>
		Console.WriteLine(((IError)option).Value.Message);
```

Tell the stream provider to do stuff:
```csharp
provider.DoStuff();
```

Await the end of the stream:
```csharp
await stream.EndOfStream;
```

## Links

Sample Project:
https://gitlab.com/reilly-digital/dotnet/enumerations.options/-/tree/main/src/Enumerations.Options.Sample

NuGet:
https://www.nuget.org/packages/ReillyDigital.Enumerations.Options
