namespace ReillyDigital.Enumerations.Options;

using System.Text.Json;

/// <summary>
/// Represents a collection of options which has an error of <see cref="Exception" />.
/// </summary>
public readonly struct OptionEnumerableError<TValue> : IOptionEnumerable<TValue>
{
	/// <summary>
	/// Implicit cast operator from a <see cref="Exception" /> that will be the reference of <see cref="Value" />.
	/// </summary>
	public static implicit operator OptionEnumerableError<TValue>(Exception value) => new(value);

	/// <summary>
	/// Implicit cast operator from a <see cref="string" /> that will create a <see cref="Exception" /> with a
	/// <see cref="Exception.Message" /> of that <see cref="string" />, and that <see cref="Exception" /> will be the
	/// reference of <see cref="Value" />.
	/// </summary>
	public static implicit operator OptionEnumerableError<TValue>(string value) => new(new(value));

	/// <summary>
	/// Implicit cast operator to a <see cref="Exception" /> which is the reference of <see cref="Value" />.
	/// </summary>
	public static implicit operator Exception(OptionEnumerableError<TValue> value) => value.Value;

	/// <summary>
	/// Static default reference for this option.
	/// </summary>
	public static readonly OptionEnumerableError<TValue> Ref = default!;

	/// <summary>
	/// Throws <see cref="Value" />.
	/// </summary>
	public IEnumerator<IOption<TValue>> IEnumerator => throw Value;

	/// <summary>
	/// The option error of <see cref="Exception" />.
	/// </summary>
	public Exception Value { get; }

	/// <summary>
	/// Constructor for this option. This will have a value of a <see cref="Exception" /> with no provided message.
	/// </summary>
	public OptionEnumerableError() => Value = new();

	/// <summary>
	/// Constructor for this option.
	/// </summary>
	/// <param name="value">The value of the option.</param>
	public OptionEnumerableError(Exception value) => Value = value;

	/// <summary>
	/// Constructor for this option. This will have a value of a <see cref="Exception" /> with the provided message.
	/// </summary>
	/// <param name="message">The error message.</param>
	/// <param name="innerException">An optional inner exception.</param>
	public OptionEnumerableError(string message, Exception? innerException = null) =>
		Value = new(message, innerException);

	/// <summary>
	/// Throws <see cref="Value" />.
	/// </summary>
	public IEnumerable<IOption<TValue>> AsEnumerable() => throw Value;

	/// <summary>
	/// Check if another object is equal to this object.
	/// </summary>
	/// <param name="other">The reference to compare.</param>
	/// <returns>A <see cref="bool" /> indicating a match.</returns>
	public bool Equals(OptionEnumerableError<TValue>? other) => other?.Value?.Equals(Value) ?? false;

	/// <summary>
	/// Throws <see cref="Value" />.
	/// </summary>
	public void ForEach(Action<IOption<TValue>> handler) => throw Value;

	/// <summary>
	/// Throws <see cref="Value" />.
	/// </summary>
	public void ForEach<TResult>(Func<IOption<TValue>, TResult> handler) => throw Value;

	/// <summary>
	/// Throws <see cref="Value" />.
	/// </summary>
	public IEnumerator<IOption<TValue>> GetEnumerator() => throw Value;

	/// <inheritdoc />
	public override int GetHashCode() => base.GetHashCode();
}

/// <summary>
/// Represents a collection of options which has an error of <see cref="TError" />.
/// </summary>
public readonly struct OptionEnumerableError<TValue, TError> : IOptionEnumerable<TValue>
{
	/// <summary>
	/// Implicit cast operator from a <see cref="TError" /> that will be the reference of <see cref="Value" />.
	/// </summary>
	public static implicit operator OptionEnumerableError<TValue, TError>(TError value) => new(value);

	/// <summary>
	/// Implicit cast operator to a <see cref="TError" /> which is the reference of <see cref="Value" />.
	/// </summary>
	public static implicit operator TError(OptionEnumerableError<TValue, TError> value) => value.Value;

	/// <summary>
	/// Static default reference for this option.
	/// </summary>
	public static readonly OptionEnumerableError<TValue, TError> Ref = default!;

	/// <summary>
	/// Throws <see cref="ValueAsException" />.
	/// </summary>
	public IEnumerator<IOption<TValue>> IEnumerator => throw ValueAsException;

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
	IEnumerator<IOption<TValue>> IOptionEnumerable<TValue>.IEnumerator => throw ValueAsException;

	/// <summary>
	/// Constructor for this option.
	/// </summary>
	/// <param name="value">The value of the option.</param>
	public OptionEnumerableError(TError value) => Value = value;

	/// <summary>
	/// Throws <see cref="ValueAsException" />.
	/// </summary>
	public IEnumerable<IOption<TValue>> AsEnumerable() => throw ValueAsException;

	/// <summary>
	/// Check if another object is equal to this object.
	/// </summary>
	/// <param name="other">The reference to compare.</param>
	/// <returns>A <see cref="bool" /> indicating a match.</returns>
	public bool Equals(OptionEnumerableError<TValue, TError>? other) => other?.Value?.Equals(Value) ?? false;

	/// <summary>
	/// Throws <see cref="ValueAsException" />.
	/// </summary>
	public void ForEach(Action<IOption<TValue>> handler) => throw ValueAsException;

	/// <summary>
	/// Throws <see cref="ValueAsException" />.
	/// </summary>
	public void ForEach<TResult>(Func<IOption<TValue>, TResult> handler) => throw ValueAsException;

	/// <summary>
	/// Throws <see cref="ValueAsException" />.
	/// </summary>
	public IEnumerator<IOption<TValue>> GetEnumerator() => throw ValueAsException;

	/// <inheritdoc />
	public override int GetHashCode() => base.GetHashCode();

	/// <summary>
	/// Throws <see cref="ValueAsException" />.
	/// </summary>
	IEnumerable<IOption<TValue>> IOptionEnumerable<TValue>.AsEnumerable() => throw ValueAsException;

	/// <summary>
	/// Throws <see cref="ValueAsException" />.
	/// </summary>
	void IOptionEnumerable<TValue>.ForEach(Action<IOption<TValue>> handler) => throw ValueAsException;

	/// <summary>
	/// Throws <see cref="ValueAsException" />.
	/// </summary>
	void IOptionEnumerable<TValue>.ForEach<TResult>(Func<IOption<TValue>, TResult> handler) =>
		throw ValueAsException;

	/// <summary>
	/// Throws <see cref="ValueAsException" />.
	/// </summary>
	IEnumerator<IOption<TValue>> IEnumerable<IOption<TValue>>.GetEnumerator() => throw ValueAsException;
}

public static partial class Functions
{
	/// <summary>
	/// Helper function to call the constructor for <see cref="OptionEnumerableError{}" />.
	/// </summary>
	/// <returns>An option of <see cref="OptionEnumerableError{}" />.</returns>
	public static OptionEnumerableError<TValue> OptionEnumerableError<TValue>() => new();

	/// <summary>
	/// Helper function to call the constructor for <see cref="OptionEnumerableError{}" />.
	/// </summary>
	/// <param name="value">The value of the option.</param>
	/// <returns>An option of <see cref="OptionEnumerableError{}" />.</returns>
	public static OptionEnumerableError<TValue> OptionEnumerableError<TValue>(Exception value) => new(value);

	/// <summary>
	/// Helper function to call the constructor for <see cref="Error{}" />.
	/// </summary>
	/// <param name="message">The error message.</param>
	/// <param name="innerException">An optional inner exception.</param>
	/// <returns>An option of <see cref="OptionEnumerableError{}" />.</returns>
	public static OptionEnumerableError<TValue> OptionEnumerableError<TValue>(
		string message, Exception? innerException = null
	) => new(message, innerException);

	/// <summary>
	/// Helper function to call the constructor for <see cref="OptionEnumerableError{,}" />.
	/// </summary>
	/// <param name="value">The value of the option.</param>
	/// <returns>An option of <see cref="OptionEnumerableError{,}" />.</returns>
	public static OptionEnumerableError<TValue, TError> OptionEnumerableError<TValue, TError>(TError value) =>
		new(value);
}
