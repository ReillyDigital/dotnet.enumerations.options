namespace ReillyDigital.Enumerations.Options.Boxed;

/// <summary>
/// Represents an option value of <see cref="TValue" /> which has a value specified.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
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
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
public interface ISome<out TValue, out TError> : IOption<TValue, TError>
{
	/// <summary>
	/// The option value of <see cref="TValue" />.
	/// </summary>
	public new TValue Value { get; }
}
