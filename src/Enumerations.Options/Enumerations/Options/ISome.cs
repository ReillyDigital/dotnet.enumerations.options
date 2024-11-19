namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents an option value of <see cref="TValue" /> which has a value specified.
/// </summary>
public interface ISome<out TValue> : IOption<TValue>
{
	/// <summary>
	/// The option value of <see cref="TValue" />.
	/// </summary>
	public new TValue Value { get; }
}

/// <summary>
/// Represents an option value of <see cref="TValue" /> which has a value specified.
/// </summary>
public interface ISome<out TValue, out TError> : IOption<TValue, TError>
{
	/// <summary>
	/// The option value of <see cref="TValue" />.
	/// </summary>
	public new TValue Value { get; }
}
