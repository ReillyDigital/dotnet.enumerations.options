namespace ReillyDigital.Enumerations.Options._Internal;

/// <summary>
/// Represents an option value of <see cref="TValue" /> which has a value specified.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
/// <param name="value">The value of the options.</param>
/// <param name="ignoredErrors">
/// Errors that are ignored instead of being returned as the option value.
/// </param>
internal readonly struct BoxedSome<TValue, TError>(
	TValue value, IEnumerable<TError>? ignoredErrors = null
) : IIgnoredErrorSet<TError>, ISome<TValue>, ISome<TValue, TError>
{
	/// <inheritdoc />
	public IEnumerable<TError> IgnoredErrors => ignoredErrors ?? [];

	/// <inheritdoc />
	public TValue Value => value;

	/// <inheritdoc />
	TValue? IOption<TValue>.Value => Value;

	/// <inheritdoc />
	TValue? IOption<TValue, TError>.Value => Value;

	/// <inheritdoc />
	public override bool Equals(object? obj)
		=> obj is BoxedSome<TValue, TError> some
		&& ((some.Value?.Equals(Value) ?? false) || (some.Value is null && Value is null));

	/// <inheritdoc />
	public override int GetHashCode() => base.GetHashCode();
}
