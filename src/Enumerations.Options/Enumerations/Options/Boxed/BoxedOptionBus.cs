namespace ReillyDigital.Enumerations.Options.Boxed;

/// <summary>
/// Represents a bus of options with a value of <see cref="TValue" /> that are accessed by
/// subscribing to events of each possible option type, triggered when an item of that type is
/// added to the bus. Errors are of type <see cref="string" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
public sealed class BoxedOptionBus<TValue> : IVoid
{
	/// <summary>
	/// An event triggered when the bus is ended.
	/// </summary>
	public event EventHandler? EndReceived;

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

	/// <summary>
	/// Returns a read-only wrapper for the current bus.
	/// </summary>
	/// <returns>A new <see cref="ReadOnlyBoxedOptionBus{TValue}" /> wrapping this bus.</returns>
	public ReadOnlyBoxedOptionBus<TValue> AsReadOnly() => new(this);

	/// <summary>
	/// A chainable call to signal the end of the bus, returning this class instance.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public BoxedOptionBus<TValue> End(IEnumerable<string>? ignoredErrors = null)
	{
		EndReceived?.Invoke(this, EventArgs.Empty);
		return this;
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="IError{TValue}" /> to the bus, returning
	/// this class instance.
	/// </summary>
	/// <param name="value">The value of an option to add to the bus.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public BoxedOptionBus<TValue> Error(
		string value, IEnumerable<string>? ignoredErrors = null
	) => Next(IOption<TValue>.Error(value, ignoredErrors: ignoredErrors));

	/// <summary>
	/// A chainable call to add an option of <see cref="IError{TValue}" /> to the bus, returning
	/// this class instance.
	/// </summary>
	/// <param name="error">The option to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public BoxedOptionBus<TValue> Error(IError<TValue> error)
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
	public BoxedOptionBus<TValue> Next(IOption<TValue> option)
	{
		OptionReceived?.Invoke(this, option);
		switch (option)
		{
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
	public BoxedOptionBus<TValue> Next(params IOption<TValue>[] options)
		=> Next(new BoxedOptionList<TValue>(options));

	/// <summary>
	/// A chainable call to add multiple options of <see cref="IOption{TValue}" /> to the bus. The
	/// options are iterated over and added to the bus one at a time. Then returning this class
	/// instance.
	/// </summary>
	/// <param name="options">The options to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public BoxedOptionBus<TValue> Next(IOptionEnumerable<TValue> options)
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
	public BoxedOptionBus<TValue> None(IEnumerable<string>? ignoredErrors = null)
		=> Next(IOption<TValue>.None(ignoredErrors: ignoredErrors));

	/// <summary>
	/// A chainable call to add an option of <see cref="ISome{TValue}" /> to the bus, returning
	/// this class instance.
	/// </summary>
	/// <param name="some">The option to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public BoxedOptionBus<TValue> Some(ISome<TValue> some)
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
	public BoxedOptionBus<TValue> Some(
		TValue value, IEnumerable<string>? ignoredErrors = null
	) => Next(IOption<TValue>.Some(value, ignoredErrors: ignoredErrors));
}

/// <summary>
/// Represents a bus of options with a value of <see cref="TValue" /> that are accessed by
/// subscribing to events of each possible option type, triggered when an item of that type is
/// added to the bus. Errors are of type <see cref="TError" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
public sealed class BoxedOptionBus<TValue, TError> : IVoid<TError>
{
	/// <summary>
	/// An event triggered when the bus is ended.
	/// </summary>
	public event EventHandler? EndReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="IError{TValue, TError}" /> is added
	/// to the bus.
	/// </summary>
	public event EventHandler<IError<TValue, TError>>? ErrorReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="INone{TValue, TError}" /> is added to
	/// the bus.
	/// </summary>
	public event EventHandler<INone<TValue, TError>>? NoneReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="IOption{TValue, TError}" /> is added
	/// to the bus.
	/// </summary>
	public event EventHandler<IOption<TValue, TError>>? OptionReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="ISome{TValue, TError}" /> is added
	/// to the bus.
	/// </summary>
	public event EventHandler<ISome<TValue, TError>>? SomeReceived;

	/// <summary>
	/// Returns a read-only wrapper for the current bus.
	/// </summary>
	/// <returns>
	/// A new <see cref="ReadOnlyBoxedOptionBus{TValue, TError}" /> wrapping this bus.
	/// </returns>
	public ReadOnlyBoxedOptionBus<TValue, TError> AsReadOnly() => new(this);

	/// <summary>
	/// A chainable call to signal the end of the bus, returning this class instance.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public BoxedOptionBus<TValue, TError> End(IEnumerable<TError>? ignoredErrors = null)
	{
		EndReceived?.Invoke(this, EventArgs.Empty);
		return this;
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="IError{TValue, TError}" /> to the bus,
	/// returning this class instance.
	/// </summary>
	/// <param name="value">The value of an option to add to the bus.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public BoxedOptionBus<TValue, TError> Error(
		TError value, IEnumerable<TError>? ignoredErrors = null
	) => Next(IOption<TValue, TError>.Error(value, ignoredErrors: ignoredErrors));

	/// <summary>
	/// A chainable call to add an option of <see cref="IError{TValue, TError}" /> to the bus,
	/// returning this class instance.
	/// </summary>
	/// <param name="error">The option to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public BoxedOptionBus<TValue, TError> Error(IError<TValue, TError> error)
	{
		if (error is IOption<TValue, TError> option)
		{
			return Next(option);
		}
		return Next(
			IOption<TValue, TError>.Error(error.Value, ignoredErrors: error.IgnoredErrors));
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="IOption{TValue, TError}" /> to the bus,
	/// returning this class instance.
	/// </summary>
	/// <param name="option">The option to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public BoxedOptionBus<TValue, TError> Next(IOption<TValue, TError> option)
	{
		OptionReceived?.Invoke(this, option);
		switch (option)
		{
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
	/// bus. The options are iterated over and added to the bus one at a time. Then returning
	/// this class instance.
	/// </summary>
	/// <param name="options">The options to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public BoxedOptionBus<TValue, TError> Next(params IOption<TValue, TError>[] options)
		=> Next(new BoxedOptionList<TValue, TError>(options));

	/// <summary>
	/// A chainable call to add multiple options of <see cref="IOption{TValue, TError}" /> to the
	/// bus. The options are iterated over and added to the bus one at a time. Then returning
	/// this class instance.
	/// </summary>
	/// <param name="options">The options to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public BoxedOptionBus<TValue, TError> Next(IOptionEnumerable<TValue, TError> options)
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
	public BoxedOptionBus<TValue, TError> None(IEnumerable<TError>? ignoredErrors = null)
		=> Next(IOption<TValue, TError>.None(ignoredErrors: ignoredErrors));

	/// <summary>
	/// A chainable call to add an option of <see cref="ISome{TValue, TError}" /> to the bus,
	/// returning this class instance.
	/// </summary>
	/// <param name="some">The option to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public BoxedOptionBus<TValue, TError> Some(ISome<TValue, TError> some)
	{
		if (some is IOption<TValue, TError> option)
		{
			return Next(option);
		}
		return Next(
			IOption<TValue, TError>.Some(some.Value, ignoredErrors: some.IgnoredErrors));
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="ISome{TValue, TError}" /> to the bus,
	/// returning this class instance.
	/// </summary>
	/// <param name="value">The value of an option to add to the bus.</param>
	/// <returns>This class instance.</returns>
	public BoxedOptionBus<TValue, TError> Some(TValue value)
		=> Next(IOption<TValue, TError>.Some(value));
}
