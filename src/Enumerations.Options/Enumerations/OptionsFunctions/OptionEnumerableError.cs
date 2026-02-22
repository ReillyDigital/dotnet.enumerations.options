namespace ReillyDigital.Enumerations;

using ReillyDigital.Enumerations.Options;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="IOptionEnumerable{TValue}.Error(IEnumerable{ErrorValue})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	public static IOptionEnumerableError<TValue> OptionEnumerableError<TValue>(
		IEnumerable<ErrorValue>? ignoredErrors = null
	) => IOptionEnumerable<TValue>.Error(ignoredErrors: ignoredErrors);

	/// <inheritdoc cref="IOptionEnumerable{TValue}.Error(ErrorValue, IEnumerable{ErrorValue})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	public static IOptionEnumerableError<TValue> OptionEnumerableError<TValue>(
		ErrorValue value, IEnumerable<ErrorValue>? ignoredErrors = null
	) => IOptionEnumerable<TValue>.Error(value, ignoredErrors: ignoredErrors);

	/// <inheritdoc cref="IOptionEnumerable{TValue, TError}.Error(TError, IEnumerable{TError})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	/// <typeparam name="TError">The type of the error of the options.</typeparam>
	public static IOptionEnumerableError<TValue, TError> OptionEnumerableError<TValue, TError>(
		TError value, IEnumerable<TError>? ignoredErrors = null
	) => IOptionEnumerable<TValue, TError>.Error(value, ignoredErrors: ignoredErrors);
}
