namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents an option with a value of <see cref="TValue" />. Errors will be of type <see cref="Exception" />.
/// </summary>
public interface IOption<out TValue> : IVoid
{
	/// <summary>
	/// Gets the value of the option. Throws an exception if the option is an error.
	/// </summary>
	TValue? Value => this switch
	{
		Error<TValue> error => throw error,
		None<TValue> => default,
		Some<TValue> some => some,
		_ => throw new InvalidOperationException("Option does not have a value.")
	};

	/// <summary>
	/// Executes the specified callback if the option is of type <see cref="End{TValue}" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current option.</returns>
	public IOption<TValue> IfEnd(Action callback)
	{
		if (this is End<TValue>)
		{
			callback();
		}
		return this;
	}

	/// <summary>
	/// Executes the specified callback if the option is of type <see cref="Error{TValue}" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current option.</returns>
	public IOption<TValue> IfError(Action callback) => IfError((_) => callback());

	/// <summary>
	/// Executes the specified callback if the option is of type <see cref="Error{TValue}" />.
	/// </summary>
	/// <param name="callback">The callback to execute with the error.</param>
	/// <returns>The current option.</returns>
	public IOption<TValue> IfError(Action<Exception> callback)
	{
		if (this is Error<TValue> error)
		{
			callback(error);
		}
		return this;
	}

	/// <summary>
	/// Executes the specified callback if the option is of type <see cref="None{TValue}" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current option.</returns>
	public IOption<TValue> IfNone(Action callback)
	{
		if (this is None<TValue>)
		{
			callback();
		}
		return this;
	}

	/// <summary>
	/// Executes the specified callback if the option is of type <see cref="Some{TValue}" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current option.</returns>
	public IOption<TValue> IfSome(Action callback) => IfSome((_) => callback());

	/// <summary>
	/// Executes the specified callback if the option is of type <see cref="Some{TValue}" />.
	/// </summary>
	/// <param name="callback">The callback to execute with the value.</param>
	/// <returns>The current option.</returns>
	public IOption<TValue> IfSome(Action<TValue> callback)
	{
		if (this is Some<TValue> some)
		{
			callback(some);
		}
		return this;
	}
}

/// <summary>
/// Represents an option with a value of <see cref="TValue" />. Errors will be of type <see cref="TError" />.
/// </summary>
public interface IOption<out TValue, out TError> : IVoid<TError>
{
	/// <summary>
	/// Gets the value of the option. Throws an exception if the option is an error.
	/// </summary>
	TValue? Value => this switch
	{
		Error<TValue, TError> error => throw error,
		None<TValue, TError> => default,
		Some<TValue, TError> some => some,
		_ => throw new InvalidOperationException("Option does not have a value.")
	};

	/// <summary>
	/// Executes the specified callback if the option is of type <see cref="End{TValue, TError}" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current option.</returns>
	public IOption<TValue, TError> IfEnd(Action callback)
	{
		if (this is End<TValue, TError>)
		{
			callback();
		}
		return this;
	}

	/// <summary>
	/// Executes the specified callback if the option is of type <see cref="Error{TValue, TError}" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current option.</returns>
	public IOption<TValue, TError> IfError(Action callback) => IfError((_) => callback());

	/// <summary>
	/// Executes the specified callback if the option is of type <see cref="Error{TValue, TError}" />.
	/// </summary>
	/// <param name="callback">The callback to execute with the error.</param>
	/// <returns>The current option.</returns>
	public IOption<TValue, TError> IfError(Action<TError> callback)
	{
		if (this is Error<TValue, TError> error)
		{
			callback(error);
		}
		return this;
	}

	/// <summary>
	/// Executes the specified callback if the option is of type <see cref="None{TValue, TError}" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current option.</returns>
	public IOption<TValue, TError> IfNone(Action callback)
	{
		if (this is None<TValue, TError>)
		{
			callback();
		}
		return this;
	}

	/// <summary>
	/// Executes the specified callback if the option is of type <see cref="Some{TValue, TError}" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current option.</returns>
	public IOption<TValue, TError> IfSome(Action callback) => IfSome((_) => callback());

	/// <summary>
	/// Executes the specified callback if the option is of type <see cref="Some{TValue, TError}" />.
	/// </summary>
	/// <param name="callback">The callback to execute with the value.</param>
	/// <returns>The current option.</returns>
	public IOption<TValue, TError> IfSome(Action<TValue> callback)
	{
		if (this is Some<TValue, TError> some)
		{
			callback(some);
		}
		return this;
	}
}
