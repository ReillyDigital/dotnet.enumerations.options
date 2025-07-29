namespace ReillyDigital.Enumerations.Options._Internal;

/// <summary>
/// Represents an option which has an error of <see cref="TError" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
/// <param name="value">The error of the options.</param>
/// <param name="ignoredErrors">
/// Errors that are ignored instead of being returned as the option value.
/// </param>
internal readonly struct OptionError<TValue, TError>(
	TError value, IEnumerable<TError>? ignoredErrors = null
) : IError<TValue>, IError<TValue, TError>, IIgnoredErrorSet<TError>
{
	/// <inheritdoc />
	public IEnumerable<TError> IgnoredErrors => ignoredErrors ?? [];

	/// <inheritdoc />
	public TError Value => value;

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

	/// <inheritdoc />
	public override bool Equals(object? obj)
		=> obj is OptionSome<TValue, TError> some
		&& ((some.Value?.Equals(Value) ?? false) || (some.Value is null && Value is null));

	/// <inheritdoc />
	public override int GetHashCode() => base.GetHashCode();
}
