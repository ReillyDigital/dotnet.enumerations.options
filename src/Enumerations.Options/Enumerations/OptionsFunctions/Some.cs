namespace ReillyDigital.Enumerations;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="IOption{TValue}.Some(TValue)" />
	public static IOption<TValue> Some<TValue>(TValue value) => IOption<TValue>.Some(value);

	/// <inheritdoc cref="IOption{TValue, TError}.Some(TValue)" />
	public static IOption<TValue, TError> Some<TValue, TError>(TValue value) => IOption<TValue, TError>.Some(value);
}
