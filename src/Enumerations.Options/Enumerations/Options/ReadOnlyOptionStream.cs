namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents a read-only stream of options with a value of <see cref="TValue" /> that are accessed by subscribing
/// to events of each possible option type, triggered when an item of that type is added to the stream. Errors are of
/// type <see cref="Exception" />.
/// </summary>
public class ReadOnlyOptionStream<TValue>(OptionStream<TValue> optionStream)
	: ReadOnlyOptionStream<TValue, Exception>(optionStream) { }

/// <summary>
/// Represents a read-only stream of options with a value of <see cref="TValue" /> that are accessed by subscribing
/// to events of each possible option type, triggered when an item of that type is added to the stream. Errors are of
/// type <see cref="TError" />.
/// </summary>
public class ReadOnlyOptionStream<TValue, TError>(OptionStream<TValue, TError> optionStream)
{
	/// <summary>
	/// An event triggered when an option of type <see cref="IEnd" /> is added to the stream.
	/// </summary>
	public event EventHandler<IOption<TValue>>? EndReceived
	{
		add => optionStream.EndReceived += value;
		remove => optionStream.EndReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="IError{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<IOption<TValue>>? ErrorReceived
	{
		add => optionStream.ErrorReceived += value;
		remove => optionStream.ErrorReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="INone" /> is added to the stream.
	/// </summary>
	public event EventHandler<IOption<TValue>>? NoneReceived
	{
		add => optionStream.NoneReceived += value;
		remove => optionStream.NoneReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="IOption{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<IOption<TValue>>? OptionReceived
	{
		add => optionStream.OptionReceived += value;
		remove => optionStream.OptionReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="ISome{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<IOption<TValue>>? SomeReceived
	{
		add => optionStream.SomeReceived += value;
		remove => optionStream.SomeReceived -= value;
	}

	/// <summary>
	/// A task that is resolved once an option of <see cref="IEnd" /> is added to the stream.
	/// </summary>
	public Task<IEnd> EndOfStream => optionStream.EndOfStream;
}
