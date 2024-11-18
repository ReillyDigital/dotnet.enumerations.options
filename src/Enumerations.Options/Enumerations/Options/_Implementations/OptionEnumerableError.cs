namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents a collection of options which has an error of <see cref="Exception" />.
/// </summary>
internal readonly struct OptionEnumerableError<TValue, TError>(TError value)
	: IError<TError>, IOptionEnumerable<TValue>
{
	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError{}.AsException()" />.
	/// </summary>
	public IEnumerator<IOption<TValue>> IEnumerator => throw ((IError<TError>)this).AsException();

	/// <summary>
	/// The option error of <see cref="TError" />.
	/// </summary>
	public TError Value => value;

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError{}.AsException()" />.
	/// </summary>
	public IEnumerable<IOption<TValue>> AsEnumerable() => throw ((IError<TError>)this).AsException();

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError{}.AsException()" />.
	/// </summary>
	public void ForEach(Action<IOption<TValue>> handler) => throw ((IError<TError>)this).AsException();

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError{}.AsException()" />.
	/// </summary>
	public void ForEach<TResult>(Func<IOption<TValue>, TResult> handler) => throw ((IError<TError>)this).AsException();

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError{}.AsException()" />.
	/// </summary>
	public IEnumerator<IOption<TValue>> GetEnumerator() => throw ((IError<TError>)this).AsException();
}
