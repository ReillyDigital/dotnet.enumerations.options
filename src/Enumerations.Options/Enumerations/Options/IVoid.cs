namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents the simplest return type of nothing or anything.
/// </summary>
public interface IVoid
{
	/// <summary>
	/// Static reference of <see cref="IVoid" />.
	/// </summary>
	public static IVoid Void => _Internal.Void<Exception>.Ref;

	/// <summary>
	/// Create a reference of <see cref="IError" />.
	/// </summary>
	/// <returns>A <see cref="IVoid" /> of <see cref="IError" />.</returns>
	public static IVoid Error() => new OptionError<IVoid, Exception>(new());

	/// <summary>
	/// Create a reference of <see cref="IError" />.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <returns>A <see cref="IVoid" /> of <see cref="IError" />.</returns>
	public static IVoid Error(Exception value) => new OptionError<IVoid, Exception>(value);

	/// <summary>
	/// Create a reference of <see cref="IError" />.
	/// </summary>
	/// <param name="message">The error message.</param>
	/// <param name="innerException">An optional inner exception.</param>
	/// <returns>A <see cref="IVoid" /> of <see cref="IError" />.</returns>
	public static IVoid Error(string message, Exception? innerException = null) =>
		new OptionError<IVoid, Exception>(new(message, innerException));

	/// <summary>
	/// Create a reference of <see cref="IError{}" />.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <returns>A <see cref="IVoid" /> of <see cref="IError" />.</returns>
	public static IVoid Error<TError>(TError value) => new OptionError<IVoid, TError>(value);

	/// <summary>
	/// Executes the specified callback if this reference is of type <see cref="IError" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current reference.</returns>
	public IVoid IfError(Action callback) => IfError((_) => callback());

	/// <summary>
	/// Executes the specified callback if this reference is of type <see cref="IError" />.
	/// </summary>
	/// <param name="callback">The callback to execute with the error.</param>
	/// <returns>The current reference.</returns>
	public IVoid IfError(Action<Exception> callback)
	{
		if (this is IError error)
		{
			callback(error.Value);
		}
		return this;
	}

	/// <summary>
	/// Executes the specified callback if this reference is of type <see cref="IError{}" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current reference.</returns>
	public IVoid IfError<TError>(Action callback) => IfError<TError>((_) => callback());

	/// <summary>
	/// Executes the specified callback if this reference is of type <see cref="IError{}" />.
	/// </summary>
	/// <param name="callback">The callback to execute with the error.</param>
	/// <returns>The current reference.</returns>
	public IVoid IfError<TError>(Action<TError> callback)
	{
		if (this is IError<IVoid, TError> error)
		{
			callback(error.Value);
		}
		return this;
	}
}

/// <summary>
/// Represents the simplest return type of nothing or anything.
/// </summary>
public interface IVoid<out TError> : IVoid
{
	/// <summary>
	/// Static reference of <see cref="IVoid" />.
	/// </summary>
	public new static IVoid<TError> Void => _Internal.Void<TError>.Ref;

	/// <summary>
	/// Create a reference of <see cref="IError{}" />.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <returns>A <see cref="IVoid" /> of <see cref="IError" />.</returns>
	public static IVoid<TError> Error(TError value) => new OptionError<IVoid, TError>(value);

	/// <summary>
	/// Executes the specified callback if this reference is of type <see cref="IError{}" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current reference.</returns>
	public new IVoid<TError> IfError(Action callback) => IfError((_) => callback());

	/// <summary>
	/// Executes the specified callback if this reference is of type <see cref="IError{}" />.
	/// </summary>
	/// <param name="callback">The callback to execute with the error.</param>
	/// <returns>The current reference.</returns>
	public IVoid<TError> IfError(Action<TError> callback)
	{
		if (this is IError<IVoid, TError> error)
		{
			callback(error.Value);
		}
		return this;
	}
}
