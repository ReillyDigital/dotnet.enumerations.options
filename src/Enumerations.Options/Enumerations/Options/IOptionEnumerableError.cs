namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents a collection of options which has an error of <see cref="Exception" />.
/// </summary>
public interface IOptionEnumerableError : IError { }

/// <summary>
/// Represents a collection of options which has an error of <see cref="Exception" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
public interface IOptionEnumerableError<out TValue> : IError<TValue>, IOptionEnumerable<TValue>
{ }

/// <summary>
/// Represents a collection of options which has an error of <see cref="TError" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
public interface IOptionEnumerableError<out TValue, out TError>
	: IError<TValue, TError>, IOptionEnumerable<TValue, TError> { }
