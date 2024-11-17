namespace ReillyDigital.Enumerations.Options._Implementations;

/// <summary>
/// Represents an option value of <see cref="TValue" /> which has a value specified.
/// </summary>
internal readonly struct OptionSome<TValue>(TValue value) : IOption<TValue>, ISome<TValue>
{
	/// <inheritdoc />
	public TValue Value => value;

	/// <inheritdoc />
	TValue? IOption<TValue>.Value => Value;
}
