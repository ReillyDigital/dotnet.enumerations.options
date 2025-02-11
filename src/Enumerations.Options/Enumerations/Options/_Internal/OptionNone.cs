namespace ReillyDigital.Enumerations.Options._Internal;

/// <summary>
/// Represents an option which has no value.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
/// <param name="ignoredErrors">
/// Errors that are ignored instead of being returned as the option value.
/// </param>
internal readonly struct OptionNone<TValue, TError>(IEnumerable<TError>? ignoredErrors = null)
	: INone<TValue>, INone<TValue, TError>
{
	/// <summary>
	/// Static default reference for this option.
	/// </summary>
	public static readonly OptionNone<TValue, TError> Ref = default;

	/// <inheritdoc />
	public IEnumerable<TError> IgnoredErrors => ignoredErrors ?? [];

	/// <inheritdoc />
	TValue? IOption<TValue>.Value => default;

	/// <inheritdoc />
	TValue? IOption<TValue, TError>.Value => default;

	/// <inheritdoc />
	public override bool Equals(object? obj) => obj is OptionEnd<TValue, TError>;

	/// <inheritdoc />
	public override int GetHashCode() => base.GetHashCode();
}
