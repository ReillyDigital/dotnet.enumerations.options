namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents an end to a sequence of options, typically used to end a <see cref="OptionStream{}" />.
/// </summary>
public interface IEnd : IVoid { }

/// <summary>
/// Represents an end to a sequence of options, typically used to end a <see cref="OptionStream{}" />.
/// </summary>
public interface IEnd<out TValue> : IEnd, IOption<TValue> { }

/// <summary>
/// Represents an end to a sequence of options, typically used to end a <see cref="OptionStream{}" />.
/// </summary>
public interface IEnd<out TValue, out TError> : IEnd, IOption<TValue, TError> { }
