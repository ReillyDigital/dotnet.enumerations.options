namespace ReillyDigital.Enumerations;

using ReillyDigital.Enumerations.Options;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="IOption{TValue}.None(IEnumerable{string})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	public static INone<TValue> BoxedNone<TValue>(IEnumerable<string>? ignoredErrors = null)
		=> IOption<TValue>.None(ignoredErrors);

	/// <inheritdoc cref="IOption{TValue, TError}.None(IEnumerable{TError})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	/// <typeparam name="TError">The type of the error of the options.</typeparam>
	public static INone<TValue, TError> BoxedNone<TValue, TError>(
		IEnumerable<TError>? ignoredErrors = null
	) => IOption<TValue, TError>.None(ignoredErrors);
}
