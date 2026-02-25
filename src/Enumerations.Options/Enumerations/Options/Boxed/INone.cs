namespace ReillyDigital.Enumerations.Options.Boxed;

/// <summary>
/// Represents an option which has no value.
/// </summary>
public interface INone : IVoid { }

/// <summary>
/// Represents an option which has no value.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
public interface INone<out TValue> : INone, IOption<TValue> { }

/// <summary>
/// Represents an option which has no value.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
public interface INone<out TValue, out TError> : INone, IOption<TValue, TError> { }
