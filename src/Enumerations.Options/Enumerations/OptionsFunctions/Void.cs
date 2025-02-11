namespace ReillyDigital.Enumerations;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="IVoid.Void(IEnumerable{Exception})" />
	public static IVoid Void(IEnumerable<Exception>? ignoredErrors = null)
		=> IVoid.Void(ignoredErrors: ignoredErrors);

	/// <inheritdoc cref="IVoid{TError}.Void(IEnumerable{TError})" />
	/// <typeparam name="TError">The type of the error of the options.</typeparam>
	public static IVoid<TError> Void<TError>(IEnumerable<TError>? ignoredErrors = null)
		=> IVoid<TError>.Void(ignoredErrors: ignoredErrors);
}
