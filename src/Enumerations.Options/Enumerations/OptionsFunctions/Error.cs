namespace ReillyDigital.Enumerations;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="IOption{}.Error()" />
	public static IOption<TValue> Error<TValue>() => IOption<TValue>.Error();

	/// <inheritdoc cref="IOption{}.Error(Exception)" />
	public static IOption<TValue> Error<TValue>(Exception value) => IOption<TValue>.Error(value);

	/// <inheritdoc cref="IOption{}.Error(string, Exception?)" />
	public static IOption<TValue> Error<TValue>(string message, Exception? innerException = null) =>
		IOption<TValue>.Error(message, innerException);

	/// <inheritdoc cref="IOption{}.Error{TError}(TError)" />
	public static IOption<TValue> Error<TValue, TError>(TError value) => IOption<TValue>.Error(value);
}
