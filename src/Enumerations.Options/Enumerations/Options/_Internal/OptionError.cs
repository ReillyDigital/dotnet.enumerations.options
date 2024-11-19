namespace ReillyDigital.Enumerations.Options._Internal;

/// <summary>
/// Represents an option which has an error of <see cref="TError" />.
/// </summary>
internal readonly struct OptionError<TValue, TError>(TError value) : IError<TValue>, IError<TValue, TError>
{
	/// <inheritdoc />
	public TError Value => value;

	/// <inheritdoc />
	Exception IError.Value => ((IError)this).Value;

	/// <inheritdoc />
	Exception IError<TValue>.Value => ((IError)this).Value;

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	TValue? IOption<TValue>.Value => throw ((IError)this).Value;

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	TValue? IOption<TValue, TError>.Value => throw ((IError)this).Value;
}
