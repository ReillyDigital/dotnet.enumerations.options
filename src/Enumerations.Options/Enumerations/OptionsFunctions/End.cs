namespace ReillyDigital.Enumerations;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="IOption{}.End" />
	public static IOption<TValue> End<TValue>() => IOption<TValue>.End;
}
