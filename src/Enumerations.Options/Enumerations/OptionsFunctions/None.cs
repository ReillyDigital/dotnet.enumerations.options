namespace ReillyDigital.Enumerations;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="IOption{}.None" />
	public static IOption<TValue> None<TValue>() => IOption<TValue>.None;
}
