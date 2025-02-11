namespace ReillyDigital.Enumerations;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="IOption{TValue}.Some(TValue, IEnumerable{Exception})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	public static IOption<TValue> Some<TValue>(
		TValue value, IEnumerable<Exception>? ignoredErrors = null
	) => IOption<TValue>.Some(value, ignoredErrors: ignoredErrors);

	/// <inheritdoc cref="IOption{TValue, TError}.Some(TValue, IEnumerable{TError})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	/// <typeparam name="TError">The type of the error of the options.</typeparam>
	public static IOption<TValue, TError> Some<TValue, TError>(
		TValue value, IEnumerable<TError>? ignoredErrors = null
	) => IOption<TValue, TError>.Some(value, ignoredErrors: ignoredErrors);
}
