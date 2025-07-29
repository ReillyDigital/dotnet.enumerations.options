namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents a read-only bus of options with a value of <see cref="TValue" /> that are
/// accessed by subscribing to events of each possible option type, triggered when an item of that
/// type is added to the bus. Errors are of
/// type <see cref="Exception" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
public sealed class ReadOnlyOptionBus<TValue>(OptionBus<TValue> optionBus) : IVoid
{
	/// <summary>
	/// An event triggered when an option of type <see cref="IEnd{TValue}" /> is added to the bus.
	/// </summary>
	public event EventHandler<IEnd<TValue>>? EndReceived
	{
		add => optionBus.EndReceived += value;
		remove => optionBus.EndReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="IError{TValue}" /> is added to the
	/// bus.
	/// </summary>
	public event EventHandler<IError<TValue>>? ErrorReceived
	{
		add => optionBus.ErrorReceived += value;
		remove => optionBus.ErrorReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="INone{TValue}" /> is added to the bus.
	/// </summary>
	public event EventHandler<INone<TValue>>? NoneReceived
	{
		add => optionBus.NoneReceived += value;
		remove => optionBus.NoneReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="IOption{TValue}" /> is added to the
	/// bus.
	/// </summary>
	public event EventHandler<IOption<TValue>>? OptionReceived
	{
		add => optionBus.OptionReceived += value;
		remove => optionBus.OptionReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="ISome{TValue}" /> is added to the bus.
	/// </summary>
	public event EventHandler<ISome<TValue>>? SomeReceived
	{
		add => optionBus.SomeReceived += value;
		remove => optionBus.SomeReceived -= value;
	}
}

/// <summary>
/// Represents a read-only bus of options with a value of <see cref="TValue" /> that are
/// accessed by subscribing to events of each possible option type, triggered when an item of that
/// type is added to the bus. Errors are of
/// type <see cref="TError" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
public sealed class ReadOnlyOptionBus<TValue, TError>(OptionBus<TValue, TError> optionBus)
	: IVoid<TError>
{
	/// <summary>
	/// An event triggered when an option of type <see cref="IEnd{TValue, TError}" /> is added to
	/// the bus.
	/// </summary>
	public event EventHandler<IEnd<TValue, TError>>? EndReceived
	{
		add => optionBus.EndReceived += value;
		remove => optionBus.EndReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="IError{TValue, TError}" /> is added to
	/// the bus.
	/// </summary>
	public event EventHandler<IError<TValue, TError>>? ErrorReceived
	{
		add => optionBus.ErrorReceived += value;
		remove => optionBus.ErrorReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="INone{TValue, TError}" /> is added to
	/// the bus.
	/// </summary>
	public event EventHandler<INone<TValue, TError>>? NoneReceived
	{
		add => optionBus.NoneReceived += value;
		remove => optionBus.NoneReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="IOption{TValue, TError}" /> is added to
	/// the bus.
	/// </summary>
	public event EventHandler<IOption<TValue, TError>>? OptionReceived
	{
		add => optionBus.OptionReceived += value;
		remove => optionBus.OptionReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type <see cref="ISome{TValue, TError}" /> is added to
	/// the bus.
	/// </summary>
	public event EventHandler<ISome<TValue, TError>>? SomeReceived
	{
		add => optionBus.SomeReceived += value;
		remove => optionBus.SomeReceived -= value;
	}
}
