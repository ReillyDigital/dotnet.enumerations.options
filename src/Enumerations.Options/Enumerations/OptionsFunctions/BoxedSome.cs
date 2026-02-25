namespace ReillyDigital.Enumerations;

using ReillyDigital.Enumerations.Options;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="IOption{TValue}.Some(TValue, IEnumerable{string})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	public static ISome<TValue> BoxedSome<TValue>(
		TValue value, IEnumerable<string>? ignoredErrors = null
	) => IOption<TValue>.Some(value, ignoredErrors);

	/// <inheritdoc cref="IOption{TValue, TError}.Some(TValue, IEnumerable{TError})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	/// <typeparam name="TError">The type of the error of the options.</typeparam>
	public static ISome<TValue, TError> BoxedSome<TValue, TError>(
		TValue value, IEnumerable<TError>? ignoredErrors = null
	) => IOption<TValue, TError>.Some(value, ignoredErrors);
}
