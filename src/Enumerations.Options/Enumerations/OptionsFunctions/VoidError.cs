namespace ReillyDigital.Enumerations;

using ReillyDigital.Enumerations.Options;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="Options.Void.Error(IEnumerable{ErrorValue})" />
	public static Void VoidError(IEnumerable<ErrorValue>? ignoredErrors = null)
		=> Options.Void.Error(ignoredErrors);

	/// <inheritdoc cref="Options.Void.Error(ErrorValue, IEnumerable{ErrorValue})" />
	public static Void VoidError(
		ErrorValue error, IEnumerable<ErrorValue>? ignoredErrors = null
	) => Options.Void.Error(error, ignoredErrors);

	/// <inheritdoc cref="Options.Void.Error(ErrorValue, IEnumerable{ErrorValue})" />
	public static Void VoidError(
		string message, IEnumerable<ErrorValue>? ignoredErrors = null
	) => Options.Void.Error(message, ignoredErrors);

	/// <inheritdoc cref="Options.Void{TError}.Error(TError, IEnumerable{TError})" />
	/// <typeparam name="TError">The type of the error of the options.</typeparam>
	public static Void<TError> VoidError<TError>(
		TError error, IEnumerable<TError>? ignoredErrors = null
	) => Options.Void<TError>.Error(error, ignoredErrors);
}
