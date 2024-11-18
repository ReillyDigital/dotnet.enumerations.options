namespace ReillyDigital.Enumerations.Options._Implementations;

/// <summary>
/// Represents an option which has no value.
/// </summary>
internal readonly struct OptionNone<TValue> : INone, IOption<TValue>
{
	/// <summary>
	/// Static default reference for this option.
	/// </summary>
	public static readonly OptionNone<TValue> Ref = default;

	/// <inheritdoc />
	TValue? IOption<TValue>.Value => default;
}
