namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents the simplest return type of nothing. This is analogous to returning <see cref="void" /> inside the
/// option pattern.
/// </summary>
public readonly struct Void : IVoid
{
	/// <summary>
	/// Static default reference for this option.
	/// </summary>
	public static readonly Void Ref = default!;

	/// <summary>
	/// Check if another object is equal to this object.
	/// </summary>
	/// <param name="other">The reference to compare.</param>
	/// <returns>A <see cref="bool" /> indicating a match.</returns>
	public bool Equals(Void? other) => other is not null;

	/// <inheritdoc />
	public override int GetHashCode() => base.GetHashCode();
}

/// <summary>
/// Represents the simplest return type of nothing. This is analogous to returning <see cref="void" /> inside the
/// option pattern.
/// </summary>
public readonly struct Void<TError> : IVoid<TError>
{
	/// <summary>
	/// Static default reference for this option.
	/// </summary>
	public static readonly Void<TError> Ref = default!;

	/// <summary>
	/// Check if another object is equal to this object.
	/// </summary>
	/// <param name="other">The reference to compare.</param>
	/// <returns>A <see cref="bool" /> indicating a match.</returns>
	public bool Equals(Void<TError>? other) => other is not null;

	/// <inheritdoc />
	public override int GetHashCode() => base.GetHashCode();
}

public static partial class Functions
{
	/// <summary>
	/// Helper function to get the static reference for <see cref="Void" />.
	/// </summary>
	/// <returns>The static reference for <see cref="Void" />.</returns>
	public static Void Void() => Options.Void.Ref;

	/// <summary>
	/// Helper function to get the static reference for <see cref="Void{}" />.
	/// </summary>
	/// <returns>The static reference for <see cref="Void{}" />.</returns>
	public static Void<TError> Void<TError>() => Options.Void<TError>.Ref;
}
