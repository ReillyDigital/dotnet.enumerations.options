namespace ReillyDigital.Enumerations;

using ReillyDigital.Enumerations.Options;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="IVoid.Error(IEnumerable{string})" />
	public static IError BoxedError(IEnumerable<string>? ignoredErrors = null)
		=> IVoid.Error(ignoredErrors);

	/// <inheritdoc cref="IVoid.Error(string, IEnumerable{string})" />
	public static IError BoxedError(
		string value, IEnumerable<string>? ignoredErrors = null
	) => IVoid.Error(value, ignoredErrors);

	/// <inheritdoc cref="IOption{TValue}.Error(IEnumerable{string})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	public static IError<TValue> BoxedError<TValue>(IEnumerable<string>? ignoredErrors = null)
		=> IOption<TValue>.Error(ignoredErrors);

	/// <inheritdoc cref="IOption{TValue}.Error(string, IEnumerable{string})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	public static IError<TValue> BoxedError<TValue>(
		string value, IEnumerable<string>? ignoredErrors = null
	) => IOption<TValue>.Error(value, ignoredErrors);

	/// <inheritdoc cref="IOption{TValue, TError}.Error(TError, IEnumerable{TError})" />
	/// <typeparam name="TValue">The type of the value of the options.</typeparam>
	/// <typeparam name="TError">The type of the error of the options.</typeparam>
	public static IError<TValue, TError> BoxedError<TValue, TError>(
		TError error, IEnumerable<TError>? ignoredErrors = null
	) => IOption<TValue, TError>.Error(error, ignoredErrors);
}
