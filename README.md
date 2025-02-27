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
		await Stream.Some("This is a streamed value.");
		await Stream.Some("This is another streamed value.");
		await Stream.Error("Oops. Streamed error.");
		await Stream.Some("One more streamed value.");
		await Stream.End();
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
		case IError error:
			Console.WriteLine(error.Value.Message);
			break;
		case ISome<string> some:
			Console.WriteLine(some.Value);
			break;
	}
}
```

### Value Pipes

Define a class that provides an option pipe.
```csharp
class PipeProvider
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
```

Get the provided pipe:
```csharp
var provider = new PipeProvider();
var pipe = provider.GetPipe();
```

Add handlers to the pipe for the various option types:
```csharp
pipe.SomeReceived +=
	(object? sender, ISome<string> some) => Console.WriteLine(some.Value);
pipe.ErrorReceived +=
	(object? sender, IError<string> error) =>
		Console.WriteLine(error.Value.Message);
```

Tell the pipe provider to do stuff:
```csharp
provider.DoStuff();
```

Await the end of the pipe:
```csharp
await pipe.Ended;
```

## Links

Sample Project:
https://gitlab.com/reilly-digital/dotnet/enumerations.options/-/tree/main/src/Enumerations.Options.Sample

NuGet:
https://www.nuget.org/packages/ReillyDigital.Enumerations.Options
