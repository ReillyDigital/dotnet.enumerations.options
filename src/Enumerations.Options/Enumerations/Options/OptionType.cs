namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// The type of option.
/// </summary>
public enum OptionType : byte
{
	/// <summary>
	/// The option is an Error.
	/// </summary>
	Error = 255,

	/// <summary>
	/// The option is a None.
	/// </summary>
	None = 2,

	/// <summary>
	/// The option is a Some.
	/// </summary>
	Some = 1
}
