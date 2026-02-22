namespace ReillyDigital.Enumerations;

using ReillyDigital.Enumerations.Options;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="Options.Void.Success(IEnumerable{ErrorValue})" />
	public static Options.Void Void(IEnumerable<ErrorValue>? ignoredErrors = null)
		=> Options.Void.Success(ignoredErrors);

	/// <inheritdoc cref="Options.Void{TError}.Success(IEnumerable{TError})" />
	/// <typeparam name="TError">The type of the error of the options.</typeparam>
	public static Options.Void<TError> Void<TError>(IEnumerable<TError>? ignoredErrors = null)
		=> Options.Void<TError>.Success(ignoredErrors);
}
