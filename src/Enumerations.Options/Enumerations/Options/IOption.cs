namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents an option with a potential value of <see cref="TValue" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
public interface IOption<out TValue> : IVoid
{
	/// <summary>
	/// Create a reference of <see cref="IError{TValue}" />.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An option of <see cref="IError{TValue}" />.</returns>
	public new static IError<TValue> Error(IEnumerable<string?>? ignoredErrors = null)
		=> new BoxedError<TValue, string?>(default, ignoredErrors: ignoredErrors);

	/// <summary>
	/// Create a reference of <see cref="IError{TValue}" />.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An option of <see cref="IError{TValue}" />.</returns>
	public new static IError<TValue> Error(
		string? value, IEnumerable<string?>? ignoredErrors = null
	) => new BoxedError<TValue, string?>(value, ignoredErrors: ignoredErrors);

	/// <summary>
	/// Create a reference of <see cref="INone{TValue}" />.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A <see cref="INone{TValue}" />.</returns>
	public static INone<TValue> None(IEnumerable<string?>? ignoredErrors = null)
		=> ignoredErrors is null ? BoxedNone<TValue, string?>.Ref : new BoxedNone<TValue, string?>(ignoredErrors);

	/// <summary>
	/// Create a reference of <see cref="ISome{TValue}" />.
	/// </summary>
	/// <param name="value">A <see cref="TValue" /> for the value of the option.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An option of <see cref="ISome{TValue}" />.</returns>
	public static ISome<TValue> Some(
		TValue value, IEnumerable<string?>? ignoredErrors = null
	) => new BoxedSome<TValue, string?>(value, ignoredErrors: ignoredErrors);

	/// <summary>
	/// The value of the option.
	/// </summary>
	public TValue? Value { get; }

	/// <inheritdoc cref="IVoid.IfError(Action)" />
	public new IOption<TValue> IfError(Action callback)
		=> (IOption<TValue>)((IVoid)this).IfError(callback);

	/// <inheritdoc cref="IVoid.IfError(Action{string})" />
	public new IOption<TValue> IfError(Action<string?> callback)
		=> (IOption<TValue>)((IVoid)this).IfError(callback);

	/// <inheritdoc cref="IVoid.IfIgnoredErrors(Action{IEnumerable{string}})" />
	public new IOption<TValue> IfIgnoredErrors(Action<IEnumerable<string?>> callback)
		=> (IOption<TValue>)((IVoid)this).IfIgnoredErrors(callback);

	/// <summary>
	/// Executes the specified callback if the option is of type <see cref="INone{TValue}" />.
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
	/// Executes the specified callback if the option is of type <see cref="ISome{TValue}" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current option.</returns>
	public IOption<TValue> IfSome(Action callback) => IfSome((_) => callback());

	/// <summary>
	/// Executes the specified callback if the option is of type <see cref="ISome{TValue}" />.
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
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
public interface IOption<out TValue, out TError> : IVoid<TError>
{
	/// <summary>
	/// Create a reference of <see cref="INone{TValue, TError}" />.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A <see cref="INone{TValue, TError}" />.</returns>
	public static INone<TValue, TError> None(IEnumerable<TError>? ignoredErrors = null)
		=> ignoredErrors is null ? BoxedNone<TValue, TError>.Ref : new BoxedNone<TValue, TError>(ignoredErrors);

	/// <summary>
	/// Create a reference of <see cref="IError{TValue, TError}" />.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An option of <see cref="IError{TValue, TError}" />.</returns>
	public new static IError<TValue, TError> Error(
		TError value, IEnumerable<TError>? ignoredErrors = null
	) => new BoxedError<TValue, TError>(value, ignoredErrors: ignoredErrors);

	/// <summary>
	/// Create a reference of <see cref="ISome{TValue, TError}" />.
	/// </summary>
	/// <param name="value">A <see cref="TValue" /> for the value of the option.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An option of <see cref="ISome{TValue, TError}" />.</returns>
	public static ISome<TValue, TError> Some(
		TValue value, IEnumerable<TError>? ignoredErrors = null
	) => new BoxedSome<TValue, TError>(value, ignoredErrors: ignoredErrors);

	/// <summary>
	/// The value of the option.
	/// </summary>
	public TValue? Value { get; }

	/// <summary>
	/// Executes the specified callback if this reference is of type <see cref="IError{TValue, TError}" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current reference.</returns>
	public new IOption<TValue, TError> IfError(Action callback) => IfError((_) => callback());

	/// <summary>
	/// Executes the specified callback if this reference is of type <see cref="IError{TValue, TError}" />.
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
	/// Executes the specified callback if the option is of type <see cref="INone{TValue, TError}" />.
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
	/// Executes the specified callback if the option is of type <see cref="ISome{TValue, TError}" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current option.</returns>
	public IOption<TValue, TError> IfSome(Action callback) => IfSome((_) => callback());

	/// <summary>
	/// Executes the specified callback if the option is of type <see cref="ISome{TValue, TError}" />.
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

	/// <inheritdoc cref="IVoid{TError}.IfIgnoredErrors(Action{IEnumerable{TError}})" />
	public new IOption<TValue, TError> IfIgnoredErrors(Action<IEnumerable<TError>> callback)
		=> (IOption<TValue, TError>)((IVoid<TError>)this).IfIgnoredErrors(callback);
}
