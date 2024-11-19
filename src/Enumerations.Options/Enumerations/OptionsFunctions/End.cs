namespace ReillyDigital.Enumerations;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="IOption{}.End" />
	public static IEnd<TValue> End<TValue>() => IOption<TValue>.End;

	/// <inheritdoc cref="IOption{,}.End" />
	public static IEnd<TValue, TError> End<TValue, TError>() => IOption<TValue, TError>.End;
}
