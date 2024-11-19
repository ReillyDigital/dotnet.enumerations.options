namespace ReillyDigital.Enumerations.Options._Internal;

/// <summary>
/// Represents the simplest return type of nothing or anything.
/// </summary>
internal readonly struct Void<TError> : IVoid<TError>
{
	/// <summary>
	/// Static default reference for this option.
	/// </summary>
	public static readonly Void<TError> Ref = default;
}
