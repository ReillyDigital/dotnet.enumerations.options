namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents an option with a value of <see cref="TValue" />. Errors will be of type <see cref="Exception" />.
/// </summary>
public interface IOption<out TValue> : IVoid
{
	/// <summary>
	/// The option value of <see cref="TValue" />.
	/// </summary>
	public TValue Value { get; }
}

/// <summary>
/// Represents an option with a value of <see cref="TValue" />. Errors will be of type <see cref="TError" />.
/// </summary>
public interface IOption<out TValue, out TError> : IVoid<TError>
{
	/// <summary>
	/// The option value of <see cref="TValue" />.
	/// </summary>
	public TValue Value { get; }
}
