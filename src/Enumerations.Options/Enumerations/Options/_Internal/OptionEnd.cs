namespace ReillyDigital.Enumerations.Options._Internal;

/// <summary>
/// Represents an end to a sequence of options, typically used to end a
/// <see cref="OptionStream{TValue}" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
/// <param name="ignoredErrors">
/// Errors that are ignored instead of being returned as the option value.
/// </param>
internal readonly struct OptionEnd<TValue, TError>(IEnumerable<TError>? ignoredErrors = null)
	: IEnd<TValue>, IEnd<TValue, TError>, IIgnoredErrorSet<TError>
{
	/// <summary>
	/// Static default reference for this option.
	/// </summary>
	public static readonly OptionEnd<TValue, TError> Ref = default;

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
	public override bool Equals(object? obj) => obj is OptionEnd<TValue, TError>;

	/// <inheritdoc />
	public override int GetHashCode() => base.GetHashCode();
}
