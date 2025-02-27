namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents a bus of options with a value of <see cref="TValue" /> that are accessed by
/// subscribing to events of each possible option type, triggered when an item of that type is
/// added to the bus. Errors are of type <see cref="Exception" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
public sealed class OptionBus<TValue> : IVoid
{
	/// <summary>
	/// An event triggered when an option of type <see cref="IEnd{TValue}" /> is added to the bus.
	/// </summary>
	public event EventHandler<IEnd<TValue>>? EndReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="IError{TValue}" /> is added to the
	/// bus.
	/// </summary>
	public event EventHandler<IError<TValue>>? ErrorReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="INone{TValue}" /> is added to the bus.
	/// </summary>
	public event EventHandler<INone<TValue>>? NoneReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="IOption{TValue}" /> is added to the
	/// bus.
	/// </summary>
	public event EventHandler<IOption<TValue>>? OptionReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="ISome{TValue}" /> is added to the bus.
	/// </summary>
	public event EventHandler<ISome<TValue>>? SomeReceived;

	/// <inheritdoc />
	public IEnumerable<Exception> IgnoredErrors => throw new(
		"Ignored errors are only supported on option bus values, not on the bus itself."
	);

	/// <summary>
	/// Returns a read-only wrapper for the current bus.
	/// </summary>
	/// <returns>A new <see cref="ReadOnlyOptionBus{TValue}" /> wrapping this bus.</returns>
	public ReadOnlyOptionBus<TValue> AsReadOnly() => new(this);

	/// <summary>
	/// A chainable call to add an option of <see cref="IEnd{TValue}" /> to the bus, returning this
	/// class instance.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue> End(IEnumerable<Exception>? ignoredErrors = null)
		=> Next(IOption<TValue>.End(ignoredErrors: ignoredErrors));

	/// <summary>
	/// A chainable call to add an option of <see cref="IError{TValue}" /> to the bus, returning
	/// this class instance.
	/// </summary>
	/// <param name="message">The error message.</param>
	/// <param name="innerException">An optional inner exception.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue> Error(
		string message, Exception? innerException = null, IEnumerable<Exception>? ignoredErrors = null
	) => Next(IOption<TValue>.Error(
		message, innerException: innerException, ignoredErrors: ignoredErrors)
	);

	/// <summary>
	/// A chainable call to add an option of <see cref="IError{TValue}" /> to the bus, returning
	/// this class instance.
	/// </summary>
	/// <param name="value">The value of an option to add to the bus.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue> Error(Exception value, IEnumerable<Exception>? ignoredErrors = null)
		=> Next(IOption<TValue>.Error(value, ignoredErrors: ignoredErrors));

	/// <summary>
	/// A chainable call to add an option of <see cref="IError{TValue}" /> to the bus, returning
	/// this class instance.
	/// </summary>
	/// <param name="error">The option to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue> Error(IError<TValue> error)
	{
		if (error is IOption<TValue> option)
		{
			return Next(option);
		}
		return Next(IOption<TValue>.Error(error.Value, ignoredErrors: error.IgnoredErrors));
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="IOption{TValue}" /> to the bus, returning
	/// this class instance.
	/// </summary>
	/// <param name="option">The option to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue> Next(IOption<TValue> option)
	{
		OptionReceived?.Invoke(this, option);
		switch (option)
		{
			case IEnd<TValue> end:
			{
				EndReceived?.Invoke(this, end);
				break;
			}
			case IError<TValue> error:
			{
				ErrorReceived?.Invoke(this, error);
				break;
			}
			case INone<TValue> none:
			{
				NoneReceived?.Invoke(this, none);
				break;
			}
			case ISome<TValue> some:
			{
				SomeReceived?.Invoke(this, some);
				break;
			}
		}
		return this;
	}

	/// <summary>
	/// A chainable call to add multiple options of <see cref="IOption{TValue}" /> to the bus. The
	/// options are iterated over and added to the bus one at a time. Then returning this class
	/// instance.
	/// </summary>
	/// <param name="options">The options to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue> Next(params IOption<TValue>[] options)
		=> Next(new OptionList<TValue>(options));

	/// <summary>
	/// A chainable call to add multiple options of <see cref="IOption{TValue}" /> to the bus. The
	/// options are iterated over and added to the bus one at a time. Then returning this class
	/// instance.
	/// </summary>
	/// <param name="options">The options to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue> Next(IOptionEnumerable<TValue> options)
	{
		options.ForEach(Next);
		return this;
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="INone{TValue}" /> to the bus, returning
	/// this class instance.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue> None(IEnumerable<Exception>? ignoredErrors = null)
		=> Next(IOption<TValue>.None(ignoredErrors: ignoredErrors));

	/// <summary>
	/// A chainable call to add an option of <see cref="ISome{TValue}" /> to the bus, returning
	/// this class instance.
	/// </summary>
	/// <param name="some">The option to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue> Some(ISome<TValue> some)
	{
		if (some is IOption<TValue> option)
		{
			return Next(option);
		}
		return Next(IOption<TValue>.Some(some.Value, ignoredErrors: some.IgnoredErrors));
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="ISome{TValue}" /> to the bus, returning
	/// this class instance.
	/// </summary>
	/// <param name="value">The value of an option to add to the bus.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue> Some(TValue value, IEnumerable<Exception>? ignoredErrors = null)
		=> Next(IOption<TValue>.Some(value, ignoredErrors: ignoredErrors));
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
	/// An event triggered when an option of type <see cref="IEnd{TValue, TError}" /> is added to
	/// the bus.
	/// </summary>
	public event EventHandler<IEnd<TValue, TError>>? EndReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="IError{TValue, TError}" /> is added to
	/// the bus.
	/// </summary>
	public event EventHandler<IError<TValue, TError>>? ErrorReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="INone{TValue, TError}" /> is added to
	/// the bus.
	/// </summary>
	public event EventHandler<INone<TValue, TError>>? NoneReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="IOption{TValue, TError}" /> is added to
	/// the bus.
	/// </summary>
	public event EventHandler<IOption<TValue, TError>>? OptionReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="ISome{TValue, TError}" /> is added to
	/// the bus.
	/// </summary>
	public event EventHandler<ISome<TValue, TError>>? SomeReceived;

	/// <inheritdoc />
	public IEnumerable<TError> IgnoredErrors => throw new(
		"Ignored errors are only supported on option bus values, not on the bus itself."
	);

	/// <summary>
	/// Returns a read-only wrapper for the current bus.
	/// </summary>
	/// <returns>
	/// A new <see cref="ReadOnlyOptionBus{TValue, TError}" /> wrapping this bus.
	/// </returns>
	public ReadOnlyOptionBus<TValue, TError> AsReadOnly() => new(this);

	/// <summary>
	/// A chainable call to add an option of <see cref="IEnd{TValue, TError}" /> to the bus,
	/// returning this class instance.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue, TError> End(IEnumerable<TError>? ignoredErrors = null)
		=> Next(IOption<TValue, TError>.End(ignoredErrors: ignoredErrors));

	/// <summary>
	/// A chainable call to add an option of <see cref="IError{TValue, TError}" /> to the bus,
	/// returning this class instance.
	/// </summary>
	/// <param name="value">The value of an option to add to the bus.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue, TError> Error(
		TError value, IEnumerable<TError>? ignoredErrors = null
	) => Next(IOption<TValue, TError>.Error(value, ignoredErrors: ignoredErrors));

	/// <summary>
	/// A chainable call to add an option of <see cref="IError{TValue, TError}" /> to the bus,
	/// returning this class instance.
	/// </summary>
	/// <param name="error">The option to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue, TError> Error(IError<TValue, TError> error)
	{
		if (error is IOption<TValue, TError> option)
		{
			return Next(option);
		}
		return Next(IOption<TValue, TError>.Error(error.Value, ignoredErrors: error.IgnoredErrors));
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="IOption{TValue, TError}" /> to the bus,
	/// returning this class instance.
	/// </summary>
	/// <param name="option">The option to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue, TError> Next(IOption<TValue, TError> option)
	{
		OptionReceived?.Invoke(this, option);
		switch (option)
		{
			case IEnd<TValue, TError> end:
			{
				EndReceived?.Invoke(this, end);
				break;
			}
			case IError<TValue, TError> error:
			{
				ErrorReceived?.Invoke(this, error);
				break;
			}
			case INone<TValue, TError> none:
			{
				NoneReceived?.Invoke(this, none);
				break;
			}
			case ISome<TValue, TError> some:
			{
				SomeReceived?.Invoke(this, some);
				break;
			}
		}
		return this;
	}

	/// <summary>
	/// A chainable call to add multiple options of <see cref="IOption{TValue, TError}" /> to the
	/// bus. The options are iterated over and added to the bus one at a time. Then returning this
	/// class instance.
	/// </summary>
	/// <param name="options">The options to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue, TError> Next(params IOption<TValue, TError>[] options)
		=> Next(new OptionList<TValue, TError>(options));

	/// <summary>
	/// A chainable call to add multiple options of <see cref="IOption{TValue, TError}" /> to the
	/// bus. The options are iterated over and added to the bus one at a time. Then returning this
	/// class instance.
	/// </summary>
	/// <param name="options">The options to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue, TError> Next(IOptionEnumerable<TValue, TError> options)
	{
		options.ForEach(Next);
		return this;
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="INone{TValue, TError}" /> to the bus,
	/// returning this class instance.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue, TError> None(IEnumerable<TError>? ignoredErrors = null)
		=> Next(IOption<TValue, TError>.None(ignoredErrors: ignoredErrors));

	/// <summary>
	/// A chainable call to add an option of <see cref="ISome{TValue, TError}" /> to the bus,
	/// returning this class instance.
	/// </summary>
	/// <param name="some">The option to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue, TError> Some(ISome<TValue, TError> some)
	{
		if (some is IOption<TValue, TError> option)
		{
			return Next(option);
		}
		return Next(IOption<TValue, TError>.Some(some.Value, ignoredErrors: some.IgnoredErrors));
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="ISome{TValue, TError}" /> to the bus,
	/// returning this class instance.
	/// </summary>
	/// <param name="value">The value of an option to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public OptionBus<TValue, TError> Some(TValue value)
		=> Next(IOption<TValue, TError>.Some(value));
}
