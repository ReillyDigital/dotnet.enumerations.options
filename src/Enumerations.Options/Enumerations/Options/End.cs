namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents an end to a sequence of options, typically used to end a <see cref="OptionStream{}" />.
/// </summary>
public readonly struct End<TValue> : IOption<TValue>
{
	/// <summary>
	/// Static default reference for this option.
	/// </summary>
	public static readonly End<TValue> Ref = default!;

	/// <summary>
	/// Constructor for this option.
	/// </summary>
	public End() { }

	/// <summary>
	/// Check if another object is equal to this object.
	/// </summary>
	/// <param name="other">The reference to compare.</param>
	/// <returns>A <see cref="bool" /> indicating a match.</returns>
	public bool Equals(End<TValue>? other) => other is not null;

	/// <inheritdoc />
	public override int GetHashCode() => base.GetHashCode();
}

/// <summary>
/// Represents an end to a sequence of options, typically used to end a <see cref="OptionStream{,}" />.
/// </summary>
public readonly struct End<TValue, TError> : IOption<TValue, TError>
{
	/// <summary>
	/// Static default reference for this option.
	/// </summary>
	public static readonly End<TValue, TError> Ref = default!;

	/// <summary>
	/// Constructor for this option.
	/// </summary>
	public End() { }

	/// <summary>
	/// Check if another object is equal to this object.
	/// </summary>
	/// <param name="other">The reference to compare.</param>
	/// <returns>A <see cref="bool" /> indicating a match.</returns>
	public bool Equals(End<TValue, TError>? other) => other is not null;

	/// <inheritdoc />
	public override int GetHashCode() => base.GetHashCode();
}

public static partial class Functions
{
	/// <summary>
	/// Helper function to get the static reference for <see cref="End{}" />.
	/// </summary>
	/// <returns>The static reference for <see cref="End{}" />.</returns>
	public static End<TValue> End<TValue>() => Options.End<TValue>.Ref;

	/// <summary>
	/// Helper function to get the static reference for <see cref="End{,}" />.
	/// </summary>
	/// <returns>The static reference for <see cref="End{,}" />.</returns>
	public static End<TValue, TError> End<TValue, TError>() => Options.End<TValue, TError>.Ref;
}
