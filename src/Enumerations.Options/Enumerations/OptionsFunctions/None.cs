namespace ReillyDigital.Enumerations;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="IOption{}.None" />
	public static INone<TValue> None<TValue>() => IOption<TValue>.None;

	/// <inheritdoc cref="IOption{,}.None" />
	public static INone<TValue, TError> None<TValue, TError>() => IOption<TValue, TError>.None;
}
