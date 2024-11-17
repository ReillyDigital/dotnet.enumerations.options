namespace ReillyDigital.Enumerations.Options._Implementations;

/// <summary>
/// Represents an option which has an error of <see cref="TError" />.
/// </summary>
internal readonly struct OptionError<TValue, TError>(TError value) : IError<TError>, IOption<TValue>
{
	/// <inheritdoc />
	public TError Value => value;

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError{}.AsException()" />.
	/// </summary>
	TValue? IOption<TValue>.Value => throw ((IError<TError>)this).AsException();
}
