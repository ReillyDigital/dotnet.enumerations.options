namespace ReillyDigital.Enumerations;

public static partial class OptionsFunctions
{
	/// <summary>
	/// Helper function to call the constructor for <see cref="IOptionEnumerableError{}" />.
	/// </summary>
	/// <returns>An option of <see cref="IOptionEnumerableError{}" />.</returns>
	public static IOptionEnumerable<TValue> OptionEnumerableError<TValue>() =>
		new OptionEnumerableError<TValue, Exception>();

	/// <summary>
	/// Helper function to call the constructor for <see cref="IOptionEnumerableError{}" />.
	/// </summary>
	/// <param name="value">The value of the option.</param>
	/// <returns>An option of <see cref="IOptionEnumerableError{}" />.</returns>
	public static IOptionEnumerable<TValue> OptionEnumerableError<TValue>(Exception value) =>
		new OptionEnumerableError<TValue, Exception>(value);

	/// <summary>
	/// Helper function to call the constructor for <see cref="IError{}" />.
	/// </summary>
	/// <param name="message">The error message.</param>
	/// <param name="innerException">An optional inner exception.</param>
	/// <returns>An option of <see cref="IOptionEnumerableError{}" />.</returns>
	public static IOptionEnumerable<TValue> OptionEnumerableError<TValue>(
		string message, Exception? innerException = null
	) => new OptionEnumerableError<TValue, Exception>(new(message, innerException));

	/// <summary>
	/// Helper function to call the constructor for <see cref="IOptionEnumerableError{}" />.
	/// </summary>
	/// <param name="value">The value of the option.</param>
	/// <returns>An option of <see cref="IOptionEnumerableError{}" />.</returns>
	public static IOptionEnumerable<TValue> OptionEnumerableError<TValue, TError>(TError value) =>
		new OptionEnumerableError<TValue, TError>(value);
}
