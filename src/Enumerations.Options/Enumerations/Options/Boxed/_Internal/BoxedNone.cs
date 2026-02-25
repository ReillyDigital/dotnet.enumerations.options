namespace ReillyDigital.Enumerations.Options.Boxed._Internal;

/// <summary>
/// Represents an option which has no value.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
/// <param name="ignoredErrors">
/// Errors that are ignored instead of being returned as the option value.
/// </param>
internal sealed class BoxedNone<TValue, TError>(IEnumerable<TError>? ignoredErrors = null)
	: IIgnoredErrorSet<TError>, INone<TValue>, INone<TValue, TError>
{
	/// <summary>
	/// Static default reference for this option.
	/// </summary>
	public static readonly BoxedNone<TValue, TError> Ref = new();

	/// <inheritdoc />
	public IEnumerable<TError> IgnoredErrors => ignoredErrors ?? [];

	/// <summary>
	/// Throws a <see cref="NotSupportedException" />.
	/// </summary>
	TValue? IOption<TValue>.Value
		=> throw new NotSupportedException("Option is not a type with a value.");

	/// <summary>
	/// Throws a <see cref="NotSupportedException" />.
	/// </summary>
	TValue? IOption<TValue, TError>.Value
		=> throw new NotSupportedException("Option is not a type with a value.");

	/// <inheritdoc />
	public override bool Equals(object? obj) => obj is BoxedNone<TValue, TError>;

	/// <inheritdoc />
	public override int GetHashCode() => base.GetHashCode();
}
