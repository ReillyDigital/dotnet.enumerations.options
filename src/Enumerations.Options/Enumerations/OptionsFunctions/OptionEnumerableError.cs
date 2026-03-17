namespace ReillyDigital.Enumerations;

using ReillyDigital.Enumerations.Options;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="OptionEnumerable{TValue}.Error(IEnumerable{string})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	public static OptionEnumerable<TValue> OptionEnumerableError<TValue>(
		IEnumerable<string>? ignoredErrors = null
	) => OptionEnumerable<TValue>.Error(ignoredErrors: ignoredErrors);

	/// <inheritdoc cref="OptionEnumerable{TValue}.Error(string, IEnumerable{string})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	public static OptionEnumerable<TValue> OptionEnumerableError<TValue>(
		string value, IEnumerable<string>? ignoredErrors = null
	) => OptionEnumerable<TValue>.Error(value, ignoredErrors: ignoredErrors);

	/// <inheritdoc cref="OptionEnumerable{TValue, TError}.Error(TError, IEnumerable{TError})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	/// <typeparam name="TError">The type of the error of the options.</typeparam>
	public static OptionEnumerable<TValue, TError> OptionEnumerableError<TValue, TError>(
		TError value, IEnumerable<TError>? ignoredErrors = null
	) => OptionEnumerable<TValue, TError>.Error(value, ignoredErrors: ignoredErrors);
}
