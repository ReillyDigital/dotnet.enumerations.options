namespace ReillyDigital.Enumerations.Options._Internal;

/// <summary>
/// Represents an option value of <see cref="TValue" /> which has a value specified.
/// </summary>
internal readonly struct OptionSome<TValue, TError>(TValue value) : ISome<TValue>, ISome<TValue, TError>
{
	/// <inheritdoc />
	public TValue Value => value;

	/// <inheritdoc />
	TValue? IOption<TValue>.Value => Value;

	/// <inheritdoc />
	TValue? IOption<TValue, TError>.Value => Value;
}
