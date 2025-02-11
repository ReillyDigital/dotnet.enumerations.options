namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents an end to a sequence of options, typically used to end a
/// <see cref="OptionStream{TValue}" />.
/// </summary>
public interface IEnd : IVoid { }

/// <summary>
/// Represents an end to a sequence of options, typically used to end a
/// <see cref="OptionStream{TValue}" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
public interface IEnd<out TValue> : IEnd, IOption<TValue> { }

/// <summary>
/// Represents an end to a sequence of options, typically used to end a
/// <see cref="OptionStream{TValue}" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
public interface IEnd<out TValue, out TError> : IEnd, IOption<TValue, TError> { }
