namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents a read-only stream of options with a value of <see cref="TValue" /> that are
/// accessed by subscribing to events of each possible option type, triggered when an item of that
/// type is added to the stream. Errors are of
/// type <see cref="Exception" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
public class ReadOnlyOptionStream<TValue>(OptionStream<TValue> optionStream) : IVoid
{
	/// <summary>
	/// An event triggered when an option of type <see cref="IEnd{TValue}" /> is added to the stream.
	/// </summary>
	public event EventHandler<IEnd<TValue>>? EndReceived
	{
		add => optionStream.EndReceived += value;
		remove => optionStream.EndReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="IError{TValue}" /> is added to the stream.
	/// </summary>
	public event EventHandler<IError<TValue>>? ErrorReceived
	{
		add => optionStream.ErrorReceived += value;
		remove => optionStream.ErrorReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="INone{TValue}" /> is added to the stream.
	/// </summary>
	public event EventHandler<INone<TValue>>? NoneReceived
	{
		add => optionStream.NoneReceived += value;
		remove => optionStream.NoneReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="IOption{TValue}" /> is added to the stream.
	/// </summary>
	public event EventHandler<IOption<TValue>>? OptionReceived
	{
		add => optionStream.OptionReceived += value;
		remove => optionStream.OptionReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="ISome{TValue}" /> is added to the stream.
	/// </summary>
	public event EventHandler<ISome<TValue>>? SomeReceived
	{
		add => optionStream.SomeReceived += value;
		remove => optionStream.SomeReceived -= value;
	}

	/// <summary>
	/// A task that is resolved once an option of <see cref="IEnd{TValue}" /> is added to the stream.
	/// </summary>
	public Task<IEnd<TValue>> EndOfStream => optionStream.EndOfStream;

	/// <inheritdoc />
	public IEnumerable<Exception> IgnoredErrors => optionStream.IgnoredErrors;
}

/// <summary>
/// Represents a read-only stream of options with a value of <see cref="TValue" /> that are
/// accessed by subscribing to events of each possible option type, triggered when an item of that
/// type is added to the stream. Errors are of
/// type <see cref="TError" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
public class ReadOnlyOptionStream<TValue, TError>(OptionStream<TValue, TError> optionStream)
	: IVoid<TError>
{
	/// <summary>
	/// An event triggered when an option of type <see cref="IEnd{TValue, TError}" /> is added to the stream.
	/// </summary>
	public event EventHandler<IEnd<TValue, TError>>? EndReceived
	{
		add => optionStream.EndReceived += value;
		remove => optionStream.EndReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="IError{TValue, TError}" /> is added to the stream.
	/// </summary>
	public event EventHandler<IError<TValue, TError>>? ErrorReceived
	{
		add => optionStream.ErrorReceived += value;
		remove => optionStream.ErrorReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="INone{TValue, TError}" /> is added to the stream.
	/// </summary>
	public event EventHandler<INone<TValue, TError>>? NoneReceived
	{
		add => optionStream.NoneReceived += value;
		remove => optionStream.NoneReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="IOption{TValue, TError}" /> is added to the stream.
	/// </summary>
	public event EventHandler<IOption<TValue, TError>>? OptionReceived
	{
		add => optionStream.OptionReceived += value;
		remove => optionStream.OptionReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="ISome{TValue, TError}" /> is added to the stream.
	/// </summary>
	public event EventHandler<ISome<TValue, TError>>? SomeReceived
	{
		add => optionStream.SomeReceived += value;
		remove => optionStream.SomeReceived -= value;
	}

	/// <summary>
	/// A task that is resolved once an option of <see cref="IEnd{TValue, TError}" /> is added to the stream.
	/// </summary>
	public Task<IEnd<TValue, TError>> EndOfStream => optionStream.EndOfStream;

	/// <inheritdoc />
	public IEnumerable<TError> IgnoredErrors => optionStream.IgnoredErrors;
}
