namespace ReillyDigital.Enumerations;

using ReillyDigital.Enumerations.Options;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="Option{TValue}.Error(IEnumerable{ErrorValue})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	public static Option<TValue> OptionError<TValue>(IEnumerable<ErrorValue>? ignoredErrors = null)
		=> Option<TValue>.Error(ignoredErrors);

	/// <inheritdoc cref="Option{TValue}.Error(ErrorValue, IEnumerable{ErrorValue})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	public static Option<TValue> OptionError<TValue>(
		ErrorValue error, IEnumerable<ErrorValue>? ignoredErrors = null
	) => Option<TValue>.Error(error, ignoredErrors);

	/// <inheritdoc cref="Option{TValue}.Error(ErrorValue, IEnumerable{ErrorValue})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	public static Option<TValue> OptionError<TValue>(
		string message, IEnumerable<ErrorValue>? ignoredErrors = null
	) => Option<TValue>.Error(message, ignoredErrors);

	/// <inheritdoc cref="Option{TValue, TError}.Error(TError, IEnumerable{TError})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	/// <typeparam name="TError">The type of the error of the options.</typeparam>
	public static Option<TValue, TError> OptionError<TValue, TError>(
		TError error, IEnumerable<TError>? ignoredErrors = null
	) => Option<TValue, TError>.Error(error, ignoredErrors);
}
