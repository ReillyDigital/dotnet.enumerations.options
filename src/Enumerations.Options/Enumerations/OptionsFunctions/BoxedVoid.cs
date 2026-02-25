namespace ReillyDigital.Enumerations;

using ReillyDigital.Enumerations.Options;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="IVoid.Void(IEnumerable{string})" />
	public static IVoid BoxedVoid(IEnumerable<string>? ignoredErrors = null)
		=> IVoid.Void(ignoredErrors);

	/// <inheritdoc cref="IVoid{TError}.Void(IEnumerable{TError})" />
	/// <typeparam name="TError">The type of the error of the options.</typeparam>
	public static IVoid<TError> BoxedVoid<TError>(IEnumerable<TError>? ignoredErrors = null)
		=> IVoid<TError>.Void(ignoredErrors);
}
