namespace ReillyDigital.Enumerations;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="IOptionEnumerable{TValue}.Error(IEnumerable{Exception})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	public static IOptionEnumerableError<TValue> OptionEnumerableError<TValue>(
		IEnumerable<Exception>? ignoredErrors = null
	) => IOptionEnumerable<TValue>.Error(ignoredErrors: ignoredErrors);

	/// <inheritdoc cref="IOptionEnumerable{TValue}.Error(Exception, IEnumerable{Exception})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	public static IOptionEnumerableError<TValue> OptionEnumerableError<TValue>(
		Exception value, IEnumerable<Exception>? ignoredErrors = null
	) => IOptionEnumerable<TValue>.Error(value, ignoredErrors: ignoredErrors);

	/// <inheritdoc
	/// 	cref="IOptionEnumerable{TValue}.Error(string, Exception?, IEnumerable{Exception})"
	/// 	/>
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	public static IOptionEnumerableError<TValue> OptionEnumerableError<TValue>(
		string message, Exception? innerException = null, IEnumerable<Exception>? ignoredErrors = null
	) => IOptionEnumerable<TValue>.Error(
		message, innerException: innerException, ignoredErrors: ignoredErrors
	);

	/// <inheritdoc cref="IOptionEnumerable{TValue, TError}.Error(TError, IEnumerable{TError})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	/// <typeparam name="TError">The type of the error of the options.</typeparam>
	public static IOptionEnumerableError<TValue, TError> OptionEnumerableError<TValue, TError>(
		TError value, IEnumerable<TError>? ignoredErrors = null
	) => IOptionEnumerable<TValue, TError>.Error(value, ignoredErrors: ignoredErrors);
}
