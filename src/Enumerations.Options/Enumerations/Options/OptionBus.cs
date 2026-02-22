namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents a bus of options with a value of <see cref="TValue" /> that are accessed by
/// subscribing to events of each possible option type, triggered when an item of that type is
/// added to the bus. Errors are of type <see cref="ErrorValue" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
public sealed class OptionBus<TValue> : IVoid
{
	/// <summary>
	/// An event triggered when the bus signals end.
	/// </summary>
	public event EventHandler? EndReceived;

	/// <summary>
	/// An event triggered when an option of type Error is added to the bus.
	/// </summary>
	public event EventHandler<Option<TValue>>? ErrorReceived;

	/// <summary>
	/// An event triggered when an option of type None is added to the bus.
	/// </summary>
	public event EventHandler<Option<TValue>>? NoneReceived;

	/// <summary>
	/// An event triggered when any option is added to the bus.
	/// </summary>
	public event EventHandler<Option<TValue>>? OptionReceived;

	/// <summary>
	/// An event triggered when an option of type Some is added to the bus.
	/// </summary>
	public event EventHandler<Option<TValue>>? SomeReceived;

	/// <summary>
	/// Returns a read-only wrapper for the current bus.
	/// </summary>
	/// <returns>A new <see cref="ReadOnlyOptionBus{TValue}" /> wrapping this bus.</returns>
	public ReadOnlyOptionBus<TValue> AsReadOnly() => new(this);

	/// <summary>
	/// Signals the end of the bus, returning this class instance.
	/// </summary>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue> End()
	{
		EndReceived?.Invoke(this, EventArgs.Empty);
		return this;
	}

	/// <summary>
	/// A chainable call to add an option of Error to the bus, returning this class instance.
	/// </summary>
	/// <param name="error">The error to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue> Error(IError error)
		=> Next(Option<TValue>.Error(error.Value, error.IgnoredErrors));

	/// <summary>
	/// A chainable call to add an option of Error to the bus, returning this class instance.
	/// </summary>
	/// <param name="value">The value of an option to add to the bus.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue> Error(ErrorValue value, IEnumerable<ErrorValue>? ignoredErrors = null)
		=> Next(Option<TValue>.Error(value, ignoredErrors));

	/// <summary>
	/// A chainable call to add an option to the bus, returning this class instance.
	/// </summary>
	/// <param name="option">The option to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue> Next(Option<TValue> option)
	{
		OptionReceived?.Invoke(this, option);
		switch (option.Type)
		{
			case OptionType.Error:
				ErrorReceived?.Invoke(this, option);
				break;
			case OptionType.None:
				NoneReceived?.Invoke(this, option);
				break;
			case OptionType.Some:
				SomeReceived?.Invoke(this, option);
				break;
		}
		return this;
	}

	/// <summary>
	/// A chainable call to add multiple options to the bus. The options are iterated over and
	/// added to the bus one at a time. Then returning this class instance.
	/// </summary>
	/// <param name="options">The options to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue> Next(params Option<TValue>[] options) => Next(options.AsEnumerable());

	/// <summary>
	/// A chainable call to add multiple options to the bus. The options are iterated over and
	/// added to the bus one at a time. Then returning this class instance.
	/// </summary>
	/// <param name="options">The options to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue> Next(IEnumerable<Option<TValue>> options)
	{
		foreach (var option in options)
		{
			Next(option);
		}
		return this;
	}

	/// <summary>
	/// A chainable call to add an option of None to the bus, returning this class instance.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue> None(IEnumerable<ErrorValue>? ignoredErrors = null)
		=> Next(Option<TValue>.None(ignoredErrors));

	/// <summary>
	/// A chainable call to add an option of Some to the bus, returning this class instance.
	/// </summary>
	/// <param name="some">The some value to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue> Some(ISome<TValue> some)
		=> Next(Option<TValue>.Some(some.Value, some.IgnoredErrors));

	/// <summary>
	/// A chainable call to add an option of Some to the bus, returning this class instance.
	/// </summary>
	/// <param name="value">The value of an option to add to the bus.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue> Some(TValue value, IEnumerable<ErrorValue>? ignoredErrors = null)
		=> Next(Option<TValue>.Some(value, ignoredErrors));
}

/// <summary>
/// Represents a bus of options with a value of <see cref="TValue" /> that are accessed by
/// subscribing to events of each possible option type, triggered when an item of that type is
/// added to the bus. Errors are of type <see cref="TError" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
public sealed class OptionBus<TValue, TError> : IVoid<TError>
{
	/// <summary>
	/// An event triggered when the bus signals end.
	/// </summary>
	public event EventHandler? EndReceived;

	/// <summary>
	/// An event triggered when an option of type Error is added to the bus.
	/// </summary>
	public event EventHandler<Option<TValue, TError>>? ErrorReceived;

	/// <summary>
	/// An event triggered when an option of type None is added to the bus.
	/// </summary>
	public event EventHandler<Option<TValue, TError>>? NoneReceived;

	/// <summary>
	/// An event triggered when any option is added to the bus.
	/// </summary>
	public event EventHandler<Option<TValue, TError>>? OptionReceived;

	/// <summary>
	/// An event triggered when an option of type Some is added to the bus.
	/// </summary>
	public event EventHandler<Option<TValue, TError>>? SomeReceived;

	/// <summary>
	/// Returns a read-only wrapper for the current bus.
	/// </summary>
	/// <returns>A new <see cref="ReadOnlyOptionBus{TValue, TError}" /> wrapping this bus.</returns>
	public ReadOnlyOptionBus<TValue, TError> AsReadOnly() => new(this);

	/// <summary>
	/// Signals the end of the bus, returning this class instance.
	/// </summary>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue, TError> End()
	{
		EndReceived?.Invoke(this, EventArgs.Empty);
		return this;
	}

	/// <summary>
	/// A chainable call to add an option of Error to the bus, returning this class instance.
	/// </summary>
	/// <param name="value">The value of an option to add to the bus.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue, TError> Error(TError value, IEnumerable<TError>? ignoredErrors = null)
		=> Next(Option<TValue, TError>.Error(value, ignoredErrors));

	/// <summary>
	/// A chainable call to add an option to the bus, returning this class instance.
	/// </summary>
	/// <param name="option">The option to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue, TError> Next(Option<TValue, TError> option)
	{
		OptionReceived?.Invoke(this, option);
		switch (option.Type)
		{
			case OptionType.Error:
				ErrorReceived?.Invoke(this, option);
				break;
			case OptionType.None:
				NoneReceived?.Invoke(this, option);
				break;
			case OptionType.Some:
				SomeReceived?.Invoke(this, option);
				break;
		}
		return this;
	}

	/// <summary>
	/// A chainable call to add multiple options to the bus. The options are iterated over and
	/// added to the bus one at a time. Then returning this class instance.
	/// </summary>
	/// <param name="options">The options to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue, TError> Next(params Option<TValue, TError>[] options)
		=> Next(options.AsEnumerable());

	/// <summary>
	/// A chainable call to add multiple options to the bus. The options are iterated over and
	/// added to the bus one at a time. Then returning this class instance.
	/// </summary>
	/// <param name="options">The options to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue, TError> Next(IEnumerable<Option<TValue, TError>> options)
	{
		foreach (var option in options)
		{
			Next(option);
		}
		return this;
	}

	/// <summary>
	/// A chainable call to add an option of None to the bus, returning this class instance.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue, TError> None(IEnumerable<TError>? ignoredErrors = null)
		=> Next(Option<TValue, TError>.None(ignoredErrors));

	/// <summary>
	/// A chainable call to add an option of Some to the bus, returning this class instance.
	/// </summary>
	/// <param name="value">The value of an option to add to the bus.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue, TError> Some(TValue value, IEnumerable<TError>? ignoredErrors = null)
		=> Next(Option<TValue, TError>.Some(value, ignoredErrors));
}
