namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents the simplest return type of nothing or anything.
/// </summary>
public interface IVoid
{
	/// <summary>
	/// Static reference of <see cref="IVoid" />.
	/// </summary>
	public static IVoid Void => _Implementations.Void.Ref;
}
