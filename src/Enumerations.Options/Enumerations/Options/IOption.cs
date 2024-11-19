namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents an option with a potential value of <see cref="TValue" />.
/// </summary>
public interface IOption<out TValue> : IVoid
{
	/// <summary>
	/// Static reference of <see cref="IEnd{}" />.
	/// </summary>
	public static IOption<TValue> End => OptionEnd<TValue, Exception>.Ref;

	/// <summary>
	/// Static reference of <see cref="INone{}" />.
	/// </summary>
	public static IOption<TValue> None => OptionNone<TValue, Exception>.Ref;

	/// <summary>
	/// Create a reference of <see cref="IError{}" />.
	/// </summary>
	/// <returns>An option of <see cref="IError{}" />.</returns>
	public new static IOption<TValue> Error() => new OptionError<TValue, Exception>(new());

	/// <summary>
	/// Create a reference of <see cref="IError{}" />.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <returns>An option of <see cref="IError{}" />.</returns>
	public new static IOption<TValue> Error(Exception value) => new OptionError<TValue, Exception>(value);

	/// <summary>
	/// Create a reference of <see cref="IError{}" />.
	/// </summary>
	/// <param name="message">The error message.</param>
	/// <param name="innerException">An optional inner exception.</param>
	/// <returns>An option of <see cref="IError{}" />.</returns>
	public new static IOption<TValue> Error(string message, Exception? innerException = null) =>
		new OptionError<TValue, Exception>(new(message, innerException));

	/// <summary>
	/// Create a reference of <see cref="IError{,}" />.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <returns>An option of <see cref="IError{,}" />.</returns>
	public new static IOption<TValue> Error<TError>(TError value) => new OptionError<TValue, TError>(value);

	/// <summary>
	/// Create a reference of <see cref="ISome{}" />.
	/// </summary>
	/// <param name="value">A <see cref="TValue" /> for the value of the option.</param>
	/// <returns>An option of <see cref="ISome{}" />.</returns>
	public static IOption<TValue> Some(TValue value) => new OptionSome<TValue, Exception>(value);

	/// <summary>
	/// The value of the option.
	/// </summary>
	public TValue? Value { get; }

	/// <summary>
	/// Executes the specified callback if the option is of type <see cref="IEnd" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current option.</returns>
	public IOption<TValue> IfEnd(Action callback)
	{
		if (this is IEnd)
		{
			callback();
		}
		return this;
	}

	/// <inheritdoc cref="IVoid.IfError(Action)" />
	public new IOption<TValue> IfError(Action callback) => (IOption<TValue>)((IVoid)this).IfError(callback);

	/// <inheritdoc cref="IVoid.Error(Exception)" />
	public new IOption<TValue> IfError(Action<Exception> callback) => (IOption<TValue>)((IVoid)this).IfError(callback);

	/// <summary>
	/// Executes the specified callback if this reference is of type <see cref="IError{}" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current reference.</returns>
	public new IOption<TValue> IfError<TError>(Action callback) => IfError<TError>((_) => callback());

	/// <summary>
	/// Executes the specified callback if this reference is of type <see cref="IError{}" />.
	/// </summary>
	/// <param name="callback">The callback to execute with the error.</param>
	/// <returns>The current reference.</returns>
	public new IOption<TValue> IfError<TError>(Action<TError> callback)
	{
		if (this is IError<TValue, TError> error)
		{
			callback(error.Value);
		}
		return this;
	}

	/// <summary>
	/// Executes the specified callback if the option is of type <see cref="INone" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current option.</returns>
	public IOption<TValue> IfNone(Action callback)
	{
		if (this is INone)
		{
			callback();
		}
		return this;
	}

	/// <summary>
	/// Executes the specified callback if the option is of type <see cref="ISome{}" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current option.</returns>
	public IOption<TValue> IfSome(Action callback) => IfSome((_) => callback());

	/// <summary>
	/// Executes the specified callback if the option is of type <see cref="ISome{}" />.
	/// </summary>
	/// <param name="callback">The callback to execute with the value.</param>
	/// <returns>The current option.</returns>
	public IOption<TValue> IfSome(Action<TValue> callback)
	{
		if (this is ISome<TValue> some)
		{
			callback(some.Value);
		}
		return this;
	}
}

/// <summary>
/// Represents an option with a potential value of <see cref="TValue" />.
/// </summary>
public interface IOption<out TValue, out TError> : IVoid<TError>
{
	/// <summary>
	/// Static reference of <see cref="IEnd" />.
	/// </summary>
	public static IOption<TValue, TError> End => OptionEnd<TValue, TError>.Ref;

	/// <summary>
	/// Static reference of <see cref="INone" />.
	/// </summary>
	public static IOption<TValue, TError> None => OptionNone<TValue, TError>.Ref;

	/// <summary>
	/// Create a reference of <see cref="IError{}" />.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <returns>An option of <see cref="IError{}" />.</returns>
	public new static IOption<TValue, TError> Error(TError value) => new OptionError<TValue, TError>(value);

	/// <summary>
	/// Create a reference of <see cref="ISome{}" />.
	/// </summary>
	/// <param name="value">A <see cref="TValue" /> for the value of the option.</param>
	/// <returns>An option of <see cref="ISome{}" />.</returns>
	public static IOption<TValue, TError> Some(TValue value) => new OptionSome<TValue, TError>(value);

	/// <summary>
	/// The value of the option.
	/// </summary>
	public TValue? Value { get; }

	/// <summary>
	/// Executes the specified callback if the option is of type <see cref="IEnd" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current option.</returns>
	public IOption<TValue, TError> IfEnd(Action callback)
	{
		if (this is IEnd)
		{
			callback();
		}
		return this;
	}

	/// <summary>
	/// Executes the specified callback if this reference is of type <see cref="IError{}" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current reference.</returns>
	public new IOption<TValue, TError> IfError(Action callback) => IfError((_) => callback());

	/// <summary>
	/// Executes the specified callback if this reference is of type <see cref="IError{}" />.
	/// </summary>
	/// <param name="callback">The callback to execute with the error.</param>
	/// <returns>The current reference.</returns>
	public new IOption<TValue, TError> IfError(Action<TError> callback)
	{
		if (this is IError<TValue, TError> error)
		{
			callback(error.Value);
		}
		return this;
	}

	/// <summary>
	/// Executes the specified callback if the option is of type <see cref="INone" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current option.</returns>
	public IOption<TValue, TError> IfNone(Action callback)
	{
		if (this is INone)
		{
			callback();
		}
		return this;
	}

	/// <summary>
	/// Executes the specified callback if the option is of type <see cref="ISome{}" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current option.</returns>
	public IOption<TValue, TError> IfSome(Action callback) => IfSome((_) => callback());

	/// <summary>
	/// Executes the specified callback if the option is of type <see cref="ISome{}" />.
	/// </summary>
	/// <param name="callback">The callback to execute with the value.</param>
	/// <returns>The current option.</returns>
	public IOption<TValue, TError> IfSome(Action<TValue> callback)
	{
		if (this is ISome<TValue> some)
		{
			callback(some.Value);
		}
		return this;
	}
}
