namespace ReillyDigital.Enumerations.Options._Internal;

/// <summary>
/// Represents an option which has no value.
/// </summary>
internal readonly struct OptionNone<TValue, TError> : INone<TValue>, INone<TValue, TError>
{
	/// <summary>
	/// Static default reference for this option.
	/// </summary>
	public static readonly OptionNone<TValue, TError> Ref = default;

	/// <inheritdoc />
	TValue? IOption<TValue>.Value => default;

	/// <inheritdoc />
	TValue? IOption<TValue, TError>.Value => default;
}
