namespace ReillyDigital.Enumerations;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="IOption{TValue}.Some(TValue)" />
	public static IOption<TValue> Some<TValue>(TValue value) => IOption<TValue>.Some(value);
}
