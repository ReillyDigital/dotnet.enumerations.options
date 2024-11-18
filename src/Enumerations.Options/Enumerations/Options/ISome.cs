namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents an option value of <see cref="TValue" /> which has a value specified.
/// </summary>
public interface ISome<TValue> : IVoid
{
	/// <summary>
	/// The option value of <see cref="TValue" />.
	/// </summary>
	public TValue Value { get; }
}
