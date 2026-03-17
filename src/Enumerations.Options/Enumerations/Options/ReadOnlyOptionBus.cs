namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents a read-only bus of options with a value of <see cref="TValue" /> that are
/// accessed by subscribing to events of each possible option type, triggered when an item of that
/// type is added to the bus. Errors are of type <see cref="Exception" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
public sealed class ReadOnlyOptionBus<TValue>(OptionBus<TValue> optionBus)
{
	/// <summary>
	/// An event triggered when the bus signals end.
	/// </summary>
	public event EventHandler? EndReceived
	{
		add => optionBus.EndReceived += value;
		remove => optionBus.EndReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type Error is added to the bus.
	/// </summary>
	public event EventHandler<Option<TValue>>? ErrorReceived
	{
		add => optionBus.ErrorReceived += value;
		remove => optionBus.ErrorReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type None is added to the bus.
	/// </summary>
	public event EventHandler<Option<TValue>>? NoneReceived
	{
		add => optionBus.NoneReceived += value;
		remove => optionBus.NoneReceived -= value;
	}

	/// <summary>
	/// An event triggered when any option is added to the bus.
	/// </summary>
	public event EventHandler<Option<TValue>>? OptionReceived
	{
		add => optionBus.OptionReceived += value;
		remove => optionBus.OptionReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type Some is added to the bus.
	/// </summary>
	public event EventHandler<Option<TValue>>? SomeReceived
	{
		add => optionBus.SomeReceived += value;
		remove => optionBus.SomeReceived -= value;
	}
}

/// <summary>
/// Represents a read-only bus of options with a value of <see cref="TValue" /> that are
/// accessed by subscribing to events of each possible option type, triggered when an item of that
/// type is added to the bus. Errors are of type <see cref="TError" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
public sealed class ReadOnlyOptionBus<TValue, TError>(OptionBus<TValue, TError> optionBus)
{
	/// <summary>
	/// An event triggered when the bus signals end.
	/// </summary>
	public event EventHandler? EndReceived
	{
		add => optionBus.EndReceived += value;
		remove => optionBus.EndReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type Error is added to the bus.
	/// </summary>
	public event EventHandler<Option<TValue, TError>>? ErrorReceived
	{
		add => optionBus.ErrorReceived += value;
		remove => optionBus.ErrorReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type None is added to the bus.
	/// </summary>
	public event EventHandler<Option<TValue, TError>>? NoneReceived
	{
		add => optionBus.NoneReceived += value;
		remove => optionBus.NoneReceived -= value;
	}

	/// <summary>
	/// An event triggered when any option is added to the bus.
	/// </summary>
	public event EventHandler<Option<TValue, TError>>? OptionReceived
	{
		add => optionBus.OptionReceived += value;
		remove => optionBus.OptionReceived -= value;
	}

	/// <summary>
	/// An event triggered when an option of type Some is added to the bus.
	/// </summary>
	public event EventHandler<Option<TValue, TError>>? SomeReceived
	{
		add => optionBus.SomeReceived += value;
		remove => optionBus.SomeReceived -= value;
	}
}
