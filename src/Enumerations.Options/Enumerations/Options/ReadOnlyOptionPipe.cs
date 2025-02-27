namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents a read-only pipe of options with a value of <see cref="TValue" /> that are
/// accessed by subscribing to events of each possible option type, triggered when an item of that
/// type is added to the pipe. Errors are of
/// type <see cref="Exception" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
public sealed class ReadOnlyOptionPipe<TValue>(OptionPipe<TValue> optionPipe) : IVoid
{
	/// <summary>
	/// An event triggered when an option of type <see cref="IEnd{TValue}" /> is added to the pipe.
	/// </summary>
	public event EventHandler<IEnd<TValue>>? EndReceived
	{
		add => optionPipe.EndReceived += value;
		remove => optionPipe.EndReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="IError{TValue}" /> is added to the
	/// pipe.
	/// </summary>
	public event EventHandler<IError<TValue>>? ErrorReceived
	{
		add => optionPipe.ErrorReceived += value;
		remove => optionPipe.ErrorReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="INone{TValue}" /> is added to the pipe.
	/// </summary>
	public event EventHandler<INone<TValue>>? NoneReceived
	{
		add => optionPipe.NoneReceived += value;
		remove => optionPipe.NoneReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="IOption{TValue}" /> is added to the
	/// pipe.
	/// </summary>
	public event EventHandler<IOption<TValue>>? OptionReceived
	{
		add => optionPipe.OptionReceived += value;
		remove => optionPipe.OptionReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="ISome{TValue}" /> is added to the pipe.
	/// </summary>
	public event EventHandler<ISome<TValue>>? SomeReceived
	{
		add => optionPipe.SomeReceived += value;
		remove => optionPipe.SomeReceived -= value;
	}

	/// <summary>
	/// A task that is resolved once an option of <see cref="IEnd{TValue}" /> is added to the pipe.
	/// </summary>
	public Task<IEnd<TValue>> Ended => optionPipe.Ended;

	/// <inheritdoc />
	public IEnumerable<Exception> IgnoredErrors => optionPipe.IgnoredErrors;
}

/// <summary>
/// Represents a read-only pipe of options with a value of <see cref="TValue" /> that are
/// accessed by subscribing to events of each possible option type, triggered when an item of that
/// type is added to the pipe. Errors are of
/// type <see cref="TError" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
public sealed class ReadOnlyOptionPipe<TValue, TError>(OptionPipe<TValue, TError> optionPipe)
	: IVoid<TError>
{
	/// <summary>
	/// An event triggered when an option of type <see cref="IEnd{TValue, TError}" /> is added to
	/// the pipe.
	/// </summary>
	public event EventHandler<IEnd<TValue, TError>>? EndReceived
	{
		add => optionPipe.EndReceived += value;
		remove => optionPipe.EndReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="IError{TValue, TError}" /> is added to
	/// the pipe.
	/// </summary>
	public event EventHandler<IError<TValue, TError>>? ErrorReceived
	{
		add => optionPipe.ErrorReceived += value;
		remove => optionPipe.ErrorReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="INone{TValue, TError}" /> is added to
	/// the pipe.
	/// </summary>
	public event EventHandler<INone<TValue, TError>>? NoneReceived
	{
		add => optionPipe.NoneReceived += value;
		remove => optionPipe.NoneReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="IOption{TValue, TError}" /> is added to
	/// the pipe.
	/// </summary>
	public event EventHandler<IOption<TValue, TError>>? OptionReceived
	{
		add => optionPipe.OptionReceived += value;
		remove => optionPipe.OptionReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="ISome{TValue, TError}" /> is added to
	/// the pipe.
	/// </summary>
	public event EventHandler<ISome<TValue, TError>>? SomeReceived
	{
		add => optionPipe.SomeReceived += value;
		remove => optionPipe.SomeReceived -= value;
	}

	/// <summary>
	/// A task that is resolved once an option of <see cref="IEnd{TValue, TError}" /> is added to
	/// the pipe.
	/// </summary>
	public Task<IEnd<TValue, TError>> Ended => optionPipe.Ended;

	/// <inheritdoc />
	public IEnumerable<TError> IgnoredErrors => optionPipe.IgnoredErrors;
}
