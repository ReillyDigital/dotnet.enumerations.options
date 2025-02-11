namespace ReillyDigital.Enumerations;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="IOption{TValue}.End(IEnumerable{Exception})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	public static IEnd<TValue> End<TValue>(IEnumerable<Exception>? ignoredErrors = null)
		=> IOption<TValue>.End(ignoredErrors: ignoredErrors);

	/// <inheritdoc cref="IOption{TValue, TError}.End(IEnumerable{TError})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	/// <typeparam name="TError">The type of the error of the options.</typeparam>
	public static IEnd<TValue, TError> End<TValue, TError>(
		IEnumerable<TError>? ignoredErrors = null
	) => IOption<TValue, TError>.End(ignoredErrors: ignoredErrors);
}
