namespace ReillyDigital.Enumerations;

using ReillyDigital.Enumerations.Options;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="Option{TValue}.None(IEnumerable{ErrorValue})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	public static Option<TValue> None<TValue>(IEnumerable<ErrorValue>? ignoredErrors = null)
		=> Option<TValue>.None(ignoredErrors);

	/// <inheritdoc cref="Option{TValue, TError}.None(IEnumerable{TError})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	/// <typeparam name="TError">The type of the error of the options.</typeparam>
	public static Option<TValue, TError> None<TValue, TError>(
		IEnumerable<TError>? ignoredErrors = null
	) => Option<TValue, TError>.None(ignoredErrors);
}
