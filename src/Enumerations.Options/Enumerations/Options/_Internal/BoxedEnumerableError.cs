namespace ReillyDigital.Enumerations.Options._Internal;

/// <summary>
/// Represents a collection of options which has an error of <see cref="TError" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
/// <param name="value">The error of the options.</param>
/// <param name="ignoredErrors">
/// Errors that are ignored instead of being returned as the option value.
/// </param>
internal readonly struct BoxedEnumerableError<TValue, TError>(
	TError value, IEnumerable<TError>? ignoredErrors = null
) : IIgnoredErrorSet<TError>, IOptionEnumerableError<TValue>, IOptionEnumerableError<TValue, TError>
{
	/// <inheritdoc />
	public IEnumerable<TError> IgnoredErrors => ignoredErrors ?? [];

	/// <summary>
	/// The option error of <see cref="TError" />.
	/// </summary>
	public TError Value => value;

	/// <inheritdoc />
	string? IError<TValue>.Value => ((IError)this).Value;

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	TValue? IOption<TValue>.Value => throw new Exception(((IError)this).Value);

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	TValue? IOption<TValue, TError>.Value => throw new Exception(((IError)this).Value);

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	IEnumerable<IOption<TValue>> IOptionEnumerable<TValue>.AsEnumerable()
		=> throw new Exception(((IError)this).Value);

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	IEnumerable<IOption<TValue, TError>> IOptionEnumerable<TValue, TError>.AsEnumerable()
		=> throw new Exception(((IError)this).Value);

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	void IOptionEnumerable<TValue>.ForEach(Action<IOption<TValue>> handler)
		=> throw new Exception(((IError)this).Value);

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	void IOptionEnumerable<TValue, TError>.ForEach(Action<IOption<TValue, TError>> handler)
		=> throw new Exception(((IError)this).Value);

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	void IOptionEnumerable<TValue>.ForEach<TResult>(Func<IOption<TValue>, TResult> handler)
		=> throw new Exception(((IError)this).Value);

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	void IOptionEnumerable<TValue, TError>.ForEach<TResult>(
		Func<IOption<TValue, TError>, TResult> handler
	) => throw new Exception(((IError)this).Value);

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		=> throw new Exception(((IError)this).Value);

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	IEnumerator<IOption<TValue>> IOptionEnumerable<TValue>.GetEnumerator()
		=> throw new Exception(((IError)this).Value);

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	IEnumerator<IOption<TValue, TError>> IOptionEnumerable<TValue, TError>.GetEnumerator()
		=> throw new Exception(((IError)this).Value);
}
