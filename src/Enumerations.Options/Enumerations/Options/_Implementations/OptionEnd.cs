namespace ReillyDigital.Enumerations.Options._Implementations;

/// <summary>
/// Represents an end to a sequence of options, typically used to end a <see cref="OptionStream{}" />.
/// </summary>
internal readonly struct OptionEnd<TValue> : IEnd, IOption<TValue>
{
	/// <summary>
	/// Static default reference for this option.
	/// </summary>
	public static readonly OptionEnd<TValue> Ref = default;

	/// <summary>
	/// Throws a <see cref="NotSupportedException" />.
	/// </summary>
	TValue? IOption<TValue>.Value => throw new NotSupportedException("Option is not a type with a value.");
}
