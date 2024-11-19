namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents a collection of options which has an error of <see cref="Exception" />.
/// </summary>
public interface IOptionEnumerableError : IError { }

/// <summary>
/// Represents a collection of options which has an error of <see cref="Exception" />.
/// </summary>
public interface IOptionEnumerableError<out TValue> : IError<TValue>, IOptionEnumerable<TValue> { }

/// <summary>
/// Represents a collection of options which has an error of <see cref="TError" />.
/// </summary>
public interface IOptionEnumerableError<out TValue, out TError>
	: IError<TValue, TError>, IOptionEnumerable<TValue, TError> { }
