namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents a read-only stream of options with a value of <see cref="TValue" /> that are accessed by subscribing
/// to events of each possible option type, triggered when an item of that type is added to the stream. Errors are of
/// type <see cref="Exception" />.
/// </summary>
public class ReadOnlyOptionStream<TValue>(OptionStream<TValue> optionStream) : IVoid
{
	/// <summary>
	/// An event triggered when an option of type <see cref="IEnd{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<IEnd<TValue>>? EndReceived
	{
		add => optionStream.EndReceived += value;
		remove => optionStream.EndReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="IError{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<IError<TValue>>? ErrorReceived
	{
		add => optionStream.ErrorReceived += value;
		remove => optionStream.ErrorReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="INone{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<INone<TValue>>? NoneReceived
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
	public event EventHandler<ISome<TValue>>? SomeReceived
	{
		add => optionStream.SomeReceived += value;
		remove => optionStream.SomeReceived -= value;
	}

	/// <summary>
	/// A task that is resolved once an option of <see cref="IEnd{}" /> is added to the stream.
	/// </summary>
	public Task<IEnd<TValue>> EndOfStream => optionStream.EndOfStream;
}

/// <summary>
/// Represents a read-only stream of options with a value of <see cref="TValue" /> that are accessed by subscribing
/// to events of each possible option type, triggered when an item of that type is added to the stream. Errors are of
/// type <see cref="TError" />.
/// </summary>
public class ReadOnlyOptionStream<TValue, TError>(OptionStream<TValue, TError> optionStream) : IVoid<TError>
{
	/// <summary>
	/// An event triggered when an option of type <see cref="IEnd{,}" /> is added to the stream.
	/// </summary>
	public event EventHandler<IEnd<TValue, TError>>? EndReceived
	{
		add => optionStream.EndReceived += value;
		remove => optionStream.EndReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="IError{,}" /> is added to the stream.
	/// </summary>
	public event EventHandler<IError<TValue, TError>>? ErrorReceived
	{
		add => optionStream.ErrorReceived += value;
		remove => optionStream.ErrorReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="INone{,}" /> is added to the stream.
	/// </summary>
	public event EventHandler<INone<TValue, TError>>? NoneReceived
	{
		add => optionStream.NoneReceived += value;
		remove => optionStream.NoneReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="IOption{,}" /> is added to the stream.
	/// </summary>
	public event EventHandler<IOption<TValue, TError>>? OptionReceived
	{
		add => optionStream.OptionReceived += value;
		remove => optionStream.OptionReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="ISome{,}" /> is added to the stream.
	/// </summary>
	public event EventHandler<ISome<TValue, TError>>? SomeReceived
	{
		add => optionStream.SomeReceived += value;
		remove => optionStream.SomeReceived -= value;
	}

	/// <summary>
	/// A task that is resolved once an option of <see cref="IEnd{,}" /> is added to the stream.
	/// </summary>
	public Task<IEnd<TValue, TError>> EndOfStream => optionStream.EndOfStream;
}
