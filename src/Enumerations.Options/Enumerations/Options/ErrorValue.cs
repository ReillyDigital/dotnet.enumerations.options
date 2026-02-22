namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents an error value.
/// </summary>
/// <param name="Message">The error message.</param>
public readonly record struct ErrorValue(string? Message = null)
{
	/// <summary>
	/// Implicitly convert a <see cref="string" /> to an <see cref="ErrorValue" />.
	/// </summary>
	/// <param name="message">The error message.</param>
	public static implicit operator ErrorValue(string message) => new(message);

	/// <summary>
	/// Implicitly convert an <see cref="ErrorValue" /> to an <see cref="Exception" />.
	/// </summary>
	/// <param name="error">The error value.</param>
	public static implicit operator Exception(ErrorValue error) => error.ToException();

	/// <summary>
	/// Convert this <see cref="ErrorValue" /> to an <see cref="Exception" />.
	/// </summary>
	/// <returns>An <see cref="Exception" /> with the error message.</returns>
	public Exception ToException() => new(Message);
}
