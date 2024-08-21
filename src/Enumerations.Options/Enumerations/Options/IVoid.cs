namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents the simplest return type of nothing or anything. Errors will be of type <see cref="Exception" />.
/// </summary>
public interface IVoid { }

/// <summary>
/// Represents the simplest return type of nothing or anything. Errors will be of type <see cref="TError" />.
/// </summary>
public interface IVoid<out TError> : IVoid { }