namespace ReillyDigital.Enumerations;

public static partial class OptionsFunctions
{
	/// <inheritdoc cref="IOptionEnumerable{}.Error()" />
	public static IOptionEnumerable<TValue> OptionEnumerableError<TValue>() => IOptionEnumerable<TValue>.Error();

	/// <inheritdoc cref="IOptionEnumerable{}.Error(Exception)" />
	public static IOptionEnumerable<TValue> OptionEnumerableError<TValue>(Exception value) =>
		IOptionEnumerable<TValue>.Error(value);

	/// <inheritdoc cref="IOptionEnumerable{}.Error(string, Exception?)" />
	public static IOptionEnumerable<TValue> OptionEnumerableError<TValue>(
		string message, Exception? innerException = null
	) => IOptionEnumerable<TValue>.Error(message, innerException);

	/// <inheritdoc cref="IOptionEnumerable{TValue, TError}.Error(TError)" />
	public static IOptionEnumerable<TValue, TError> OptionEnumerableError<TValue, TError>(TError value) =>
		IOptionEnumerable<TValue, TError>.Error(value);
}
