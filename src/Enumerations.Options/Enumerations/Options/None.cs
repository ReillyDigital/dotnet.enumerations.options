namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents an option which has no value.
/// </summary>
public readonly struct None<TValue> : IOption<TValue>
{
	/// <summary>
	/// Static default reference for this option.
	/// </summary>
	public static readonly None<TValue> Ref = default!;

	/// <summary>
	/// Constructor for this option.
	/// </summary>
	public None() { }

	/// <summary>
	/// Check if another object is equal to this object.
	/// </summary>
	/// <param name="other">The reference to compare.</param>
	/// <returns>A <see cref="bool" /> indicating a match.</returns>
	public bool Equals(None<TValue>? other) => other is not null;

	/// <inheritdoc />
	public override int GetHashCode() => base.GetHashCode();
}

public static partial class Functions
{
	/// <summary>
	/// Helper function to get the static reference for <see cref="None{}" />.
	/// </summary>
	/// <returns>The static reference for <see cref="None{}" />.</returns>
	public static None<TValue> None<TValue>() => Options.None<TValue>.Ref;
}
