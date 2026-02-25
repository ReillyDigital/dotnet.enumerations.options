namespace ReillyDigital.Enumerations;

using ReillyDigital.Enumerations.Options;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="Options.Void.Error(IEnumerable{string})" />
	public static Void VoidError(IEnumerable<string>? ignoredErrors = null)
		=> Options.Void.Error(ignoredErrors);

	/// <inheritdoc cref="Options.Void.Error(string, IEnumerable{string})" />
	public static Void VoidError(
		string error, IEnumerable<string>? ignoredErrors = null
	) => Options.Void.Error(error, ignoredErrors);

	/// <inheritdoc cref="Options.Void{TError}.Error(TError, IEnumerable{TError})" />
	/// <typeparam name="TError">The type of the error of the options.</typeparam>
	public static Void<TError> VoidError<TError>(
		TError error, IEnumerable<TError>? ignoredErrors = null
	) => Options.Void<TError>.Error(error, ignoredErrors);
}
