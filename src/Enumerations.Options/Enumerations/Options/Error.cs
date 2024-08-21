namespace ReillyDigital.Enumerations.Options;

using System.Text.Json;

/// <summary>
/// Represents an option which has an error of <see cref="Exception" />.
/// </summary>
public readonly struct Error<TValue> : IOption<TValue>
{
	/// <summary>
	/// Implicit cast operator from a <see cref="Exception" /> that will be the reference of <see cref="Value" />.
	/// </summary>
	public static implicit operator Error<TValue>(Exception value) => new(value);

	/// <summary>
	/// Implicit cast operator from a <see cref="string" /> that will create a <see cref="Exception" /> with a
	/// <see cref="Exception.Message" /> of that <see cref="string" />, and that <see cref="Exception" /> will be the
	/// reference of <see cref="Value" />.
	/// </summary>
	public static implicit operator Error<TValue>(string value) => new(new(value));

	/// <summary>
	/// Implicit cast operator to a <see cref="Exception" /> which is the reference of <see cref="Value" />.
	/// </summary>
	public static implicit operator Exception(Error<TValue> value) => value.Value;

	/// <summary>
	/// Static default reference for this option.
	/// </summary>
	public static readonly Error<TValue> Ref = default!;

	/// <summary>
	/// The option error of <see cref="Exception" />.
	/// </summary>
	public Exception Value { get; }

	/// <summary>
	/// Throws <see cref="Value" />.
	/// </summary>
	TValue IOption<TValue>.Value => throw Value;

	/// <summary>
	/// Constructor for this option. This will have a value of a <see cref="Exception" /> with no provided message.
	/// </summary>
	public Error() => Value = new();

	/// <summary>
	/// Constructor for this option.
	/// </summary>
	/// <param name="value">The value of the option.</param>
	public Error(Exception value) => Value = value;

	/// <summary>
	/// Constructor for this option. This will have a value of a <see cref="Exception" /> with the provided message.
	/// </summary>
	/// <param name="message">The error message.</param>
	/// <param name="innerException">An optional inner exception.</param>
	public Error(string message, Exception? innerException = null) => Value = new(message, innerException);

	/// <summary>
	/// Check if another object is equal to this object.
	/// </summary>
	/// <param name="other">The reference to compare.</param>
	/// <returns>A <see cref="bool" /> indicating a match.</returns>
	public bool Equals(Error<TValue>? other) => other?.Value.Equals(Value) ?? false;

	/// <inheritdoc />
	public override int GetHashCode() => base.GetHashCode();
}

/// <summary>
/// Represents an option which has an error of <see cref="TError" />.
/// </summary>
public readonly struct Error<TValue, TError> : IOption<TValue, TError>
{
	/// <summary>
	/// Implicit cast operator from a <see cref="TError" /> that will be the reference of <see cref="Value" />.
	/// </summary>
	public static implicit operator Error<TValue, TError>(TError value) => new(value);

	/// <summary>
	/// Implicit cast operator to a <see cref="TError" /> which is the reference of <see cref="Value" />.
	/// </summary>
	public static implicit operator TError(Error<TValue, TError> value) => value.Value;

	/// <summary>
	/// Static default reference for this option.
	/// </summary>
	public static readonly Error<TValue, TError> Ref = default!;

	/// <summary>
	/// The option error of <see cref="TError" />.
	/// </summary>
	public TError Value { get; }

	/// <summary>
	/// The option error as a <see cref="Exception" />, with an inner exception of the serialized <see cref="Value" />
	/// if serializable.
	/// </summary>
#pragma warning disable SYSLIB0050
	private Exception ValueAsException => new(
		$"Value is an error of type {typeof(TError).FullName}.",
		typeof(TError).IsSerializable ? new(JsonSerializer.Serialize(Value)) : null
	);
#pragma warning restore SYSLIB0050

	/// <summary>
	/// Throws <see cref="ValueAsException" />.
	/// </summary>
	TValue IOption<TValue, TError>.Value => throw ValueAsException;

	/// <summary>
	/// Constructor for this option.
	/// </summary>
	/// <param name="value">The value of the option.</param>
	public Error(TError value) => Value = value;

	/// <summary>
	/// Check if another object is equal to this object.
	/// </summary>
	/// <param name="other">The reference to compare.</param>
	/// <returns>A <see cref="bool" /> indicating a match.</returns>
	public bool Equals(Error<TValue, TError>? other) => other?.Value?.Equals(Value) ?? false;

	/// <inheritdoc />
	public override int GetHashCode() => base.GetHashCode();
}

public static partial class Functions
{
	/// <summary>
	/// Helper function to call the constructor for <see cref="Error{}" />.
	/// </summary>
	/// <returns>An option of <see cref="Error{}" />.</returns>
	public static Error<TValue> Error<TValue>() => new();

	/// <summary>
	/// Helper function to call the constructor for <see cref="Error{}" />.
	/// </summary>
	/// <param name="value">The value of the option.</param>
	/// <returns>An option of <see cref="Error{}" />.</returns>
	public static Error<TValue> Error<TValue>(Exception value) => new(value);

	/// <summary>
	/// Helper function to call the constructor for <see cref="Error{}" />.
	/// </summary>
	/// <param name="message">The error message.</param>
	/// <param name="innerException">An optional inner exception.</param>
	/// <returns>An option of <see cref="Error{}" />.</returns>
	public static Error<TValue> Error<TValue>(string message, Exception? innerException = null) =>
		new(message, innerException);

	/// <summary>
	/// Helper function to call the constructor for <see cref="Error{,}" />.
	/// </summary>
	/// <param name="value">The value of the option.</param>
	/// <returns>An option of <see cref="Error{,}" />.</returns>
	public static Error<TValue, TError> Error<TValue, TError>(TError value) => new(value);
}
