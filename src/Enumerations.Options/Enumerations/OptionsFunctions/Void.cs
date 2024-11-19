namespace ReillyDigital.Enumerations;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="IVoid.Void" />
	public static IVoid Void() => IVoid.Void;

	/// <inheritdoc cref="IVoid{}.Void" />
	public static IVoid<TError> Void<TError>() => IVoid<TError>.Void;
}
