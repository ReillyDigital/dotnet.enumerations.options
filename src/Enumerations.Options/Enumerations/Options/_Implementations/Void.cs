namespace ReillyDigital.Enumerations.Options._Implementations;

/// <summary>
/// Represents the simplest return type of nothing or anything.
/// </summary>
internal readonly struct Void : IVoid
{
	/// <summary>
	/// Static default reference for this option.
	/// </summary>
	public static readonly Void Ref = default;
}
