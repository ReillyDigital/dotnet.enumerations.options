namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents a read-only stream of options with a value of <see cref="TValue" /> that are accessed by subscribing
/// to events of each possible option type, triggered when an item of that type is added to the stream.
/// </summary>
public sealed class ReadOnlyOptionStream<TValue>(OptionStream<TValue> OptionStream)
{
	/// <summary>
	/// An event triggered when an option of type <see cref="End{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<End<TValue>>? EndReceived
	{
		add => OptionStream.EndReceived += value;
		remove => OptionStream.EndReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="Error{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<Error<TValue>>? ErrorReceived
	{
		add => OptionStream.ErrorReceived += value;
		remove => OptionStream.ErrorReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="None{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<None<TValue>>? NoneReceived
	{
		add => OptionStream.NoneReceived += value;
		remove => OptionStream.NoneReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="IOption{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<IOption<TValue>>? OptionReceived
	{
		add => OptionStream.OptionReceived += value;
		remove => OptionStream.OptionReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="Some{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<Some<TValue>>? SomeReceived
	{
		add => OptionStream.SomeReceived += value;
		remove => OptionStream.SomeReceived -= value;
	}

	/// <summary>
	/// A task that is resolved once an option of <see cref="End{}" /> is added to the stream.
	/// </summary>
	public Task<End<TValue>> EndOfStream => OptionStream.EndOfStream;
}

/// <summary>
/// Represents a read-only stream of options with a value of <see cref="TValue" /> that are accessed by subscribing
/// to events of each possible option type, triggered when an item of that type is added to the stream.
/// </summary>
public sealed class ReadOnlyOptionStream<TValue, TError>(OptionStream<TValue, TError> OptionStream)
{
	/// <summary>
	/// An event triggered when an option of type <see cref="End{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<End<TValue>>? EndReceived
	{
		add => OptionStream.EndReceived += value;
		remove => OptionStream.EndReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="Error{,}" /> is added to the stream.
	/// </summary>
	public event EventHandler<Error<TValue, TError>>? ErrorReceived
	{
		add => OptionStream.ErrorReceived += value;
		remove => OptionStream.ErrorReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="None{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<None<TValue>>? NoneReceived
	{
		add => OptionStream.NoneReceived += value;
		remove => OptionStream.NoneReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="IOption{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<IOption<TValue>>? OptionReceived
	{
		add => OptionStream.OptionReceived += value;
		remove => OptionStream.OptionReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="Some{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<Some<TValue>>? SomeReceived
	{
		add => OptionStream.SomeReceived += value;
		remove => OptionStream.SomeReceived -= value;
	}

	/// <summary>
	/// A task that is resolved once an option of <see cref="End{} "/> is added to the stream.
	/// </summary>
	public Task<End<TValue>> EndOfStream => OptionStream.EndOfStream;
}
