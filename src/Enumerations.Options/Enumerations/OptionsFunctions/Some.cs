namespace ReillyDigital.Enumerations;

using ReillyDigital.Enumerations.Options;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="Option{TValue}.Some(TValue, IEnumerable{string})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	public static Option<TValue> Some<TValue>(
		TValue value, IEnumerable<string?>? ignoredErrors = null
	) => Option<TValue>.Some(value, ignoredErrors);

	/// <inheritdoc cref="Option{TValue, TError}.Some(TValue, IEnumerable{TError})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	/// <typeparam name="TError">The type of the error of the options.</typeparam>
	public static Option<TValue, TError> Some<TValue, TError>(
		TValue value, IEnumerable<TError>? ignoredErrors = null
	) => Option<TValue, TError>.Some(value, ignoredErrors);
}
