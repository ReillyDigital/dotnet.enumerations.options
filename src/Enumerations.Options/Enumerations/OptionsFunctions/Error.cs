namespace ReillyDigital.Enumerations;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="IOption{TValue}.Error(IEnumerable{Exception})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	public static IError<TValue> Error<TValue>(IEnumerable<Exception>? ignoredErrors = null)
		=> IOption<TValue>.Error(ignoredErrors: ignoredErrors);

	/// <inheritdoc cref="IOption{TValue}.Error(Exception, IEnumerable{Exception})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	public static IError<TValue> Error<TValue>(
		Exception value, IEnumerable<Exception>? ignoredErrors = null
	) => IOption<TValue>.Error(value, ignoredErrors: ignoredErrors);

	/// <inheritdoc cref="IOption{TValue}.Error(string, Exception?, IEnumerable{Exception})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	public static IError<TValue> Error<TValue>(
		string message, Exception? innerException = null, IEnumerable<Exception>? ignoredErrors = null
	) => IOption<TValue>.Error(message, innerException, ignoredErrors: ignoredErrors);

	/// <inheritdoc cref="IOption{TValue, TError}.Error(TError, IEnumerable{TError})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	/// <typeparam name="TError">The type of the error of the options.</typeparam>
	public static IError<TValue, TError> Error<TValue, TError>(
		TError value, IEnumerable<TError>? ignoredErrors = null
	) => IOption<TValue, TError>.Error(value, ignoredErrors: ignoredErrors);
}
