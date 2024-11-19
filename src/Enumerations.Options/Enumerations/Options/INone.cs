namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents an option which has no value.
/// </summary>
public interface INone : IVoid { }

/// <summary>
/// Represents an option which has no value.
/// </summary>
public interface INone<out TValue> : INone, IOption<TValue> { }

/// <summary>
/// Represents an option which has no value.
/// </summary>
public interface INone<out TValue, out TError> : INone, IOption<TValue, TError> { }
