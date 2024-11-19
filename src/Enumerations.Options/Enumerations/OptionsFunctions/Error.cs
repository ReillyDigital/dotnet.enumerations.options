namespace ReillyDigital.Enumerations;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="IOption{}.Error()" />
	public static IError<TValue> Error<TValue>() => IOption<TValue>.Error();

	/// <inheritdoc cref="IOption{}.Error(Exception)" />
	public static IError<TValue> Error<TValue>(Exception value) => IOption<TValue>.Error(value);

	/// <inheritdoc cref="IOption{}.Error(string, Exception?)" />
	public static IError<TValue> Error<TValue>(string message, Exception? innerException = null) =>
		IOption<TValue>.Error(message, innerException);

	/// <inheritdoc cref="IOption{TValue, TError}.Error(TError)" />
	public static IError<TValue, TError> Error<TValue, TError>(TError value) => IOption<TValue, TError>.Error(value);
}
