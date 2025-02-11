namespace ReillyDigital.Enumerations;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="IOption{TValue}.None(IEnumerable{Exception})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	public static INone<TValue> None<TValue>(IEnumerable<Exception>? ignoredErrors = null)
		=> IOption<TValue>.None(ignoredErrors: ignoredErrors);

	/// <inheritdoc cref="IOption{TValue, TError}.None(IEnumerable{TError})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	/// <typeparam name="TError">The type of the error of the options.</typeparam>
	public static INone<TValue, TError> None<TValue, TError>(
		IEnumerable<TError>? ignoredErrors = null
	) => IOption<TValue, TError>.None(ignoredErrors: ignoredErrors);
}
