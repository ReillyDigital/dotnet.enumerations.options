namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents an option value of <see cref="TValue" /> which has a value specified.
/// </summary>
public readonly struct Some<TValue> : IOption<TValue>
{
	/// <summary>
	/// Implicit cast operator from a <see cref="TValue" /> that will be the reference of <see cref="Value" />.
	/// </summary>
	public static implicit operator Some<TValue>(TValue value) => new(value);

	/// <summary>
	/// Implicit cast operator to a <see cref="TValue" /> which is the reference of <see cref="Value" />.
	/// </summary>
	public static implicit operator TValue(Some<TValue> value) => value.Value;

	/// <summary>
	/// Static default reference for this option.
	/// </summary>
	public static readonly Some<TValue> Ref = default!;

	/// <summary>
	/// The option value of <see cref="TValue" />.
	/// </summary>
	public TValue Value { get; }

	/// <summary>
	/// Constructor for this option.
	/// </summary>
	/// <param name="value">A <see cref="TValue" /> for the value of the option.</param>
	public Some(TValue value) => Value = value;

	/// <summary>
	/// Check if another object is equal to this object.
	/// </summary>
	/// <param name="other">The reference to compare.</param>
	/// <returns>A <see cref="bool" /> indicating a match.</returns>
	public bool Equals(Some<TValue>? other) => other is not null && Value!.Equals(other.Value);

	/// <inheritdoc />
	public override int GetHashCode() => base.GetHashCode();
}

/// <summary>
/// Represents an option value of <see cref="TValue" /> which has a value specified.
/// </summary>
public readonly struct Some<TValue, TError> : IOption<TValue, TError>
{
	/// <summary>
	/// Implicit cast operator from a <see cref="TValue" /> that will be the reference of <see cref="Value" />.
	/// </summary>
	public static implicit operator Some<TValue, TError>(TValue value) => new(value);

	/// <summary>
	/// Implicit cast operator to a <see cref="TValue" /> which is the reference of <see cref="Value" />.
	/// </summary>
	public static implicit operator TValue(Some<TValue, TError> value) => value.Value;

	/// <summary>
	/// Static default reference for this option.
	/// </summary>
	public static readonly Some<TValue, TError> Ref = default!;

	/// <summary>
	/// The option value of <see cref="TValue" />.
	/// </summary>
	public TValue Value { get; }

	/// <summary>
	/// Constructor for this option.
	/// </summary>
	/// <param name="value">A <see cref="TValue" /> for the value of the option.</param>
	public Some(TValue value) => Value = value;

	/// <summary>
	/// Check if another object is equal to this object.
	/// </summary>
	/// <param name="other">The reference to compare.</param>
	/// <returns>A <see cref="bool" /> indicating a match.</returns>
	public bool Equals(Some<TValue, TError>? other) => other is not null && Value!.Equals(other.Value);

	/// <inheritdoc />
	public override int GetHashCode() => base.GetHashCode();
}

public static partial class Functions
{
	/// <summary>
	/// Helper function to call the constructor for <see cref="Some{}" />.
	/// </summary>
	/// <param name="value">A <see cref="TValue" /> for the value of the option.</param>
	/// <returns>An option of <see cref="Some{}" />.</returns>
	public static Some<TValue> Some<TValue>(TValue value) => new(value);

	/// <summary>
	/// Helper function to call the constructor for <see cref="Some{,}" />.
	/// </summary>
	/// <param name="value">A <see cref="TValue" /> for the value of the option.</param>
	/// <returns>An option of <see cref="Some{,}" />.</returns>
	public static Some<TValue, TError> Some<TValue, TError>(TValue value) => new(value);
}
