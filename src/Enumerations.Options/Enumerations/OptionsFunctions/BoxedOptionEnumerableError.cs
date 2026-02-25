namespace ReillyDigital.Enumerations;

using ReillyDigital.Enumerations.Options;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="IOptionEnumerable{TValue}.Error(IEnumerable{string})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	public static IOptionEnumerableError<TValue> BoxedOptionEnumerableError<TValue>(
		IEnumerable<string>? ignoredErrors = null
	) => IOptionEnumerable<TValue>.Error(ignoredErrors);

	/// <inheritdoc cref="IOptionEnumerable{TValue}.Error(string, IEnumerable{string})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	public static IOptionEnumerableError<TValue> BoxedOptionEnumerableError<TValue>(
		string value, IEnumerable<string>? ignoredErrors = null
	) => IOptionEnumerable<TValue>.Error(value, ignoredErrors);

	/// <inheritdoc cref="IOptionEnumerable{TValue, TError}.Error(TError, IEnumerable{TError})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	/// <typeparam name="TError">The type of the error of the options.</typeparam>
	public static IOptionEnumerableError<TValue, TError> BoxedOptionEnumerableError<TValue, TError>(
		TError value, IEnumerable<TError>? ignoredErrors = null
	) => IOptionEnumerable<TValue, TError>.Error(value, ignoredErrors);
}
